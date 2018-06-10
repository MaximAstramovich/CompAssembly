using DAL.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class AbstractRepository : IDisposable
    {
        protected NewCADBContext caContext;
        public AbstractRepository()
        {
            caContext = new NewCADBContext();
        }

        public void Dispose()
        {
            caContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
