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
                Address = source.Address,
                BankCode = source.BankCode,
                CheckingAccount = source.CheckingAccount,
                FIO = source.FIO,
                Firm = source.Firm,
                IdSuppliers = source.IdSuppliers,
                PhoneNumber = source.PhoneNumber,
                Position = source.Position,
                UNN = source.UNN
            };
        }

        SuppliersModel ToObject(Suppliers source)
        {
            return new SuppliersModel()
            {
                Address = source.Address,
                BankCode = source.BankCode,
                CheckingAccount = source.CheckingAccount,
                FIO = source.FIO,
                Firm = source.Firm,
                IdSuppliers = source.IdSuppliers,
                PhoneNumber = source.PhoneNumber,
                Position = source.Position,
                UNN = source.UNN
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
            var entity = this.caContext.Suppliers.FirstOrDefault(x => x.IdSuppliers == item.IdSuppliers);
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

        public void Update(SuppliersModel source)
        {
            var entity = this.caContext.Suppliers.FirstOrDefault(x => x.IdSuppliers == source.IdSuppliers);
            if (entity != null)
            {
                entity.Address = source.Address;
                entity.BankCode = source.BankCode;
                entity.CheckingAccount = source.CheckingAccount;
                entity.FIO = source.FIO;
                entity.Firm = source.Firm;
                entity.IdSuppliers = source.IdSuppliers;
                entity.PhoneNumber = source.PhoneNumber;
                entity.Position = source.Position;
                entity.UNN = source.UNN;
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public Suppliers GetEntity(SuppliersModel source)
        {
            var entity = this.caContext.Suppliers.FirstOrDefault(x => x.IdSuppliers == source.IdSuppliers);
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

        public List<Receipts> GetAllReceiptsBySupplierId(int idSupplier)
        {
            var supplier = caContext.Suppliers.Find(idSupplier);
            return supplier.Receipts.ToList();
        }

        public void SaveChanges()
        {
            caContext.SaveChanges();
        }
    }
}
