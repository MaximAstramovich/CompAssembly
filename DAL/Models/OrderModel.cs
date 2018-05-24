using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class OrderModel
    {
        public int IdOrder { get; set; }
        public int? NumberAssembly { get; set; }
        public System.DateTime? DateOfPayment { get; set; }
    }
}
