using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicines.DataProcessor.ImportDtos
{
    public class ImportPatientDto
    {
        [Required]
        //[Range(5, 100)]
        [MinLength(5)]
        [MaxLength(100)]
        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [Required]
        [JsonProperty("AgeGroup")]
        public int AgeGroup { get; set; }

        [Required]
        [JsonProperty("Gender")]
        public int Gender { get; set; }

        [JsonProperty("Medicines")]
        public int[] Medicines { get; set; }

    }
}
