using DAL.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    class AbstractRepository : IDisposable
    {
        protected CA_DBContext caContext;
        public AbstractRepository()
        {
            caContext = new CA_DBContext();
        }

        public void Dispose()
        {
            caContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
