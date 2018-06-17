using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ComponentsModel
    {
        public int IDCOM { get; set; }
        public int? Type { get; set; }
        public string Nazv { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }

        public virtual ComponentTypesModel ComponentTypes { get; set; }
        public virtual StockModel Stock { get; set; }
        public virtual List<ReceiptsModel> Receipts { get; set; }

        public override string ToString()
        {
            return this.Nazv;
        }
    }
}
