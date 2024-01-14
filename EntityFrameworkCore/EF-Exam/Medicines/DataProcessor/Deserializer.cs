namespace Medicines.DataProcessor
{
    using Boardgames.Helpers;
    using Invoices.Extensions;
    using Medicines.Data;
    using Medicines.Data.Models;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ImportDtos;
    using Medicines.Extensions;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Text;


    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedPharmacy = "Successfully imported pharmacy - {0} with {1} medicines.";
        private const string SuccessfullyImportedPatient = "Successfully imported patient - {0} with {1} medicines.";

        public static string ImportPatients(MedicinesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ImportPatientDto[] patientsDtos = JsonSerializationExtension.DeserializeFromJson<ImportPatientDto[]>(jsonString);

            List<Patient> patients = new List<Patient>();

            int[] medsIds = context.Medicines.Select(m => m.Id).ToArray();


            foreach (var pat in patientsDtos)
            {
                if (!IsValid(pat))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var patient = new Patient()
                {
                    FullName = pat.FullName,
                    AgeGroup =(AgeGroup) pat.AgeGroup,
                    Gender =(Gender) pat.Gender,
                };
                foreach (var meds in pat.Medicines.Distinct())
                {
                    if (medsIds.Contains(meds))
                    {
                        patient.PatientsMedicines.Add(new PatientMedicine()
                        {
                            MedicineId = meds
                        });
                    }
                    else
                    {
                        sb.AppendLine(ErrorMessage);
                    }

                }
                patients.Add(patient);
                sb.AppendLine(string.Format(SuccessfullyImportedPatient, patient.FullName, patient.PatientsMedicines.Count));
            }

            context.Patients.AddRange(patients);
            context.SaveChanges();
            return sb.ToString();
        }

        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            List<ImportPharmacyDto> pharmacyDtos = xmlString
                .DeserializeFromXml<List<ImportPharmacyDto>>("Clients");

            StringBuilder sb = new();

            List<Pharmacy> pharmacyList = new();


            foreach (var pharmacyDto in pharmacyDtos)
            {
                if (!IsValid(pharmacyDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue ;
                }

                Pharmacy pharmacy = new()
                {
                    Name = pharmacyDto.Name,
                    PhoneNumber = pharmacyDto.PhoneNumber,
                    IsNonStop = pharmacyDto.IsNonStop,
                };

                foreach (var MedicineDto in pharmacyDto.Medicines)
                {
                    if (!IsValid(MedicineDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    pharmacy.Medicines.Add(new Medicine()
                    {
                        Name = MedicineDto.Name,
                        Price = MedicineDto.Price,
                        ProductionDate = MedicineDto.ProductionDate,
                        ExpiryDate = MedicineDto.ExpiryDate,
                        Producer = MedicineDto.Producer,
                        Category = (Category)MedicineDto.Category
                    });


                }

                pharmacyList.Add(pharmacy);
                sb.AppendLine(string.Format(SuccessfullyImportedPharmacy, pharmacy.Name, pharmacy.Medicines.Count));
            }


            context.Pharmacies.AddRange(pharmacyList);
            context.SaveChanges();

            return sb.ToString();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
