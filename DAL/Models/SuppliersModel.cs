using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SuppliersModel
    {
        public int IdSuppliers { get; set; }
        public string Firm { get; set; }
        public string Address { get; set; }
        public int? UNN { get; set; }
        public int? CheckingAccount { get; set; }
        public string BankCode { get; set; }
        public string FIO { get; set; }
        public string Position { get; set; }
        public int? PhoneNumber { get; set; }
    }
}
