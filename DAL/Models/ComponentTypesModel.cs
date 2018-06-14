using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ComponentTypesModel
    {
        public int ID { get; set; }
        public string Type { get; set; }

        public List<ComponentsModel> Components { get; set; }
    }
}
