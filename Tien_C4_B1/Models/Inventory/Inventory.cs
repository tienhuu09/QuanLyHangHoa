using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tien_C4_B1
{
    public class Inventory
    {
        public List<ImportInventory> lstImport { get; set; }
        public List<ExportInventory> lstExport { get; set; }
        public List<RemainingProduct> lstRemain { get; set; }

        public Inventory()
        {
            lstImport = new List<ImportInventory>();
            lstExport = new List<ExportInventory>();
            lstRemain = new List<RemainingProduct>();
        }

    }
}
