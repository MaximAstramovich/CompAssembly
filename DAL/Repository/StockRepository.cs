using DAL.DBModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class StockRepository : AbstractRepository, IModelRepository<StockModel, Stock>
    {
        Stock ToEntity(StockModel source)
        {
            return new Stock()
            {
                IDCOM = source.IDCOM,
                InStock = source.InStock
            };
        }

        StockModel ToObject(Stock source)
        {
            return new StockModel()
            {
                IDCOM = source.IDCOM,
                InStock = source.InStock
            };
        }
        public void Add(StockModel item)
        {
            var entity = this.ToEntity(item);
            caContext.Stock.Add(entity);
            SaveChanges();
        }

        public void Remove(StockModel item)
        {
            var entity = this.caContext.Stock.FirstOrDefault(x => x.IDCOM == item.IDCOM);
            if (entity != null)
            {
                caContext.Stock.Remove(entity);
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public void Update(StockModel item)
        {
            var entity = this.caContext.Stock.FirstOrDefault(x => x.IDCOM == item.IDCOM);
            if (entity != null)
            {
                entity.IDCOM = item.IDCOM;
                entity.InStock = item.InStock;
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public Stock GetEntity(StockModel source)
        {
            var entity = this.caContext.Stock.FirstOrDefault(x => x.IDCOM == source.IDCOM);
            return entity;
        }

        public IEnumerable<StockModel> Items
        {
            get
            {
                var modelsList = new List<StockModel>();
                foreach (var entity in caContext.Stock.Select(x => x))
                {
                    modelsList.Add(ToObject(entity));
                }

                return modelsList;
            }
        }

        public void SaveChanges()
        {
            caContext.SaveChanges();
        }
    }
}
