using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class CustomersModel
    {
        public int IDCUS { get; set; }
        public string FIO { get; set; }
        public System.DateTime? DateOfBirth { get; set; }
        public string PassportNo { get; set; }
        public System.DateTime? DateOfIssue { get; set; }
        public string Authority { get; set; }
        public string Address { get; set; }
        public int? PhoneNumber { get; set; }

        public List<AssemblyModel> Assembly { get; set; }
        public List<SellingModel> Selling { get; set; }
    }
}
