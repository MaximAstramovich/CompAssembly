using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AssemblyModel
    {
        public int IdAssembly { get; set; }
        public int? IDCUS { get; set; }
        public System.DateTime? OrderDate { get; set; }
        public int? Num { get; set; }
        public decimal? Summ { get; set; }
        public int? Corpus { get; set; }
        public int? Board { get; set; }
        public int? CPU { get; set; }
        public int? Graphic { get; set; }
        public int? OZU { get; set; }
        public int? HDD { get; set; }
        public int? SSD { get; set; }
        public int? Power { get; set; }
        public int? DVD { get; set; }
        public int? Audio { get; set; }
        public int? Ice { get; set; }
        public int? Status { get; set; }
        public System.DateTime? DateOfPayment { get; set; }

        public CustomersModel Customers { get; set; }
    }
}
