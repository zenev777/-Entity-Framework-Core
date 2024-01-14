using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Pharmacy")]
    public class ImportPharmacyDto
    {
        [XmlAttribute("IsNonStop")]
        public bool IsNonStop { get; set; }


        [Required]
        [MinLength(2)]
        [MaxLength(50)]       
        public string Name { get; set; }


        [Required]
        [StringLength(14)]
      
        public string PhoneNumber { get; set; }

        [XmlArray("Medicines")]
        public ImportMedcineDto[] Medicines { get; set; }


    
    }
}
