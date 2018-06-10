using DAL.DBModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class SellingRepository : AbstractRepository, IModelRepository<SellingModel, Selling>
    {
        Selling ToEntity(SellingModel source)
        {
            return new Selling()
            {
                IdSelling = source.IDS,
                IdCom = source.IDCOM,
                IdCustomer = source.IDCUS,
                Price = source.Price,
                Quality = source.Quality,
                DateOfSale = source.DateOfSale
            };
        }

        SellingModel ToObject(Selling source)
        {
            return new SellingModel()
            {
                IDS = source.IdSelling,
                IDCOM = source.IdCom,
                IDCUS = source.IdCustomer,
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
            var entity = this.caContext.Selling.FirstOrDefault(x => x.IdSelling == item.IDS);
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
            var entity = this.caContext.Selling.FirstOrDefault(x => x.IdSelling == item.IDS);
            if (entity != null)
            {
                entity.IdCom = item.IDCOM;
                entity.IdCustomer = item.IDCUS;
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
            var entity = this.caContext.Selling.FirstOrDefault(x => x.IdSelling == source.IDS);
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

        public Customers GetCustomerBySellingId(int idSelling)
        {
            return caContext.Selling.Find(idSelling).Customers;
        }

        public void SaveChanges()
        {
            caContext.SaveChanges();
        }
    }
}
