using DAL.DBModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    class SellingRepository : AbstractRepository, IModelRepository<SellingModel, Selling>
    {
        Selling ToEntity(SellingModel source)
        {
            return new Selling()
            {
                IDS = source.IDS,
                IDCOM = source.IDCOM,
                IDCUS = source.IDCUS,
                Price = source.Price,
                Quality = source.Quality,
                DateOfSale = source.DateOfSale
            };
        }

        SellingModel ToObject(Selling source)
        {
            return new SellingModel()
            {
                IDS = source.IDS,
                IDCOM = source.IDCOM,
                IDCUS = source.IDCUS,
                Price = source.Price,
                Quality = source.Quality,
                DateOfSale = source.DateOfSale
            };
        }
        public void Add(SellingModel item)
        {
            var entity = this.ToEntity(item);
            caContext.Selling.Add(entity);
            SaveChanges();
        }

        public void Remove(SellingModel item)
        {
            var entity = this.caContext.Selling.FirstOrDefault(x => x.IDS == item.IDS);
            if (entity != null)
            {
                caContext.Selling.Remove(entity);
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public void Update(SellingModel item)
        {
            var entity = this.caContext.Selling.FirstOrDefault(x => x.IDS == item.IDS);
            if (entity != null)
            {
                entity.IDS = item.IDS;
                entity.IDCOM = item.IDCOM;
                entity.IDCUS = item.IDCUS;
                entity.Price = item.Price;
                entity.Quality = item.Quality;
                entity.DateOfSale = item.DateOfSale;
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public Selling GetEntity(SellingModel source)
        {
            var entity = this.caContext.Selling.FirstOrDefault(x => x.IDS == source.IDS);
            return entity;
        }

        public IEnumerable<SellingModel> Items
        {
            get
            {
                var modelsList = new List<SellingModel>();
                foreach (var entity in caContext.Selling.Select(x => x))
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
