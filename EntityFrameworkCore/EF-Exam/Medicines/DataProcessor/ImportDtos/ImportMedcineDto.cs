using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ImportDtos
{

    [XmlType("Medicine")]
    public class ImportMedcineDto
    {
        

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [Range(0.01, 1000.00)]
        [XmlElement("Price")]
        public decimal Price { get; set; }

        [XmlAttribute("Category")]
        public int Category { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
        [XmlElement("ProductionDate")]
        public DateTime ProductionDate { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
        [XmlElement("ExpiryDate")]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        [XmlElement("Producer")]
        public string Producer { get; set; }
    }
}