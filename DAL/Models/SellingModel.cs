using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    class SellingModel
    {
        public int IDS { get; set; }
        public int? IDCUS { get; set; }
        public int? IDCOM { get; set; }
        public int? Quality { get; set; }
        public int? Price { get; set; }
        public string DateOfSale { get; set; }
    }
}
