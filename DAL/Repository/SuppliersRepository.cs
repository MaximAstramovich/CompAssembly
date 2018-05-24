using DAL.DBModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class SuppliersRepository : AbstractRepository, IModelRepository<SuppliersModel, Suppliers>
    {
        Suppliers ToEntity(SuppliersModel source)
        {
            return new Suppliers()
            {
                FIO = source.FIO,
                IDSUP = source.IDSUP,
                Firm = source.Firm,
                PhoneNumber = source.PhoneNumber,
                Position = source.Position
            };
        }

        SuppliersModel ToObject(Suppliers source)
        {
            return new SuppliersModel()
            {
                FIO = source.FIO,
                IDSUP = source.IDSUP,
                Firm = source.Firm,
                PhoneNumber = source.PhoneNumber,
                Position = source.Position
            };
        }
        public void Add(SuppliersModel item)
        {
            var entity = this.ToEntity(item);
            caContext.Suppliers.Add(entity);
            SaveChanges();
        }

        public void Remove(SuppliersModel item)
        {
            var entity = this.caContext.Suppliers.FirstOrDefault(x => x.IDSUP == item.IDSUP);
            if (entity != null)
            {
                caContext.Suppliers.Remove(entity);
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public void Update(SuppliersModel item)
        {
            var entity = this.caContext.Suppliers.FirstOrDefault(x => x.IDSUP == item.IDSUP);
            if (entity != null)
            {
                entity.IDSUP = item.IDSUP;
                entity.FIO = item.FIO;
                entity.Firm = item.Firm;
                entity.PhoneNumber = item.PhoneNumber;
                entity.Position = item.Position;
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public Suppliers GetEntity(SuppliersModel source)
        {
            var entity = this.caContext.Suppliers.FirstOrDefault(x => x.IDSUP == source.IDSUP);
            return entity;
        }

        public IEnumerable<SuppliersModel> Items
        {
            get
            {
                var modelsList = new List<SuppliersModel>();
                foreach (var entity in caContext.Suppliers.Select(x => x))
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
