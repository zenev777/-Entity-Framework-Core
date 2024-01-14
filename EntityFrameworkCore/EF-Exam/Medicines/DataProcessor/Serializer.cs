namespace Medicines.DataProcessor
{
    using Medicines.Extensions;
    using Medicines.Data;
    using Medicines.DataProcessor.ExportDtos;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {
            throw new ArgumentException();
        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies(MedicinesContext context, int medicineCategory)
        {

            var medicines = context.Medicines
                .Where(p=>p.Pharmacy.IsNonStop == true)
                .Select(p => new
                {
                    Name = p.Name,
                    Price = p.Price.ToString("f2"),
                    Pharmacy =p.Pharmacy.Medicines
                        .Select(m => new
                        {
                            m.Pharmacy.Name,
                            m.Pharmacy.PhoneNumber
                        }).ToArray()
                }).OrderBy(p=>p.Price).ThenBy(n=>n.Name)
                .ToArray();


            var json = JsonConvert.SerializeObject(medicines);
            return json;
        }
    }
}
