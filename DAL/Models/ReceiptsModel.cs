using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ReceiptsModel
    {
        public int IDR { get; set; }
        public int? IDSUP { get; set; }
        public int? IDCOM { get; set; }
        public int? Quality { get; set; }
        public decimal? Price { get; set; }
        public System.DateTime? ReceiptDate { get; set; }
        public SuppliersModel Supplier { get; set; }
        public ComponentsModel Component { get; set; }
    }
}
