using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IModelRepository<T, K>
    {
        void Add(T item);
        void Remove(T item);
        void Update(T item);
        K GetEntity(T source);
        IEnumerable<T> Items { get; }
        void SaveChanges();
    }
}
