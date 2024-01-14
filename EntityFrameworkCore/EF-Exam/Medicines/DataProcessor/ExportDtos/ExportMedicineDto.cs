using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicines.DataProcessor.ExportDtos
{
    public class ExportMedicineDto
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public ExportPharmacytDto[] Pharmacies { get; set; }
    }
}
