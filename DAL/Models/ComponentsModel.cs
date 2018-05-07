using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    class ComponentsModel
    {
        public int IDCOM { get; set; }
        public int? Type { get; set; }
        public string Nazv { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
    }
}
