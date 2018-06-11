using DAL.DBModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class CustomersRepository : AbstractRepository, IModelRepository<CustomersModel, Customers>
    {
        Customers ToEntity(CustomersModel source, bool isIdIncluded = false)
        {
            if (!isIdIncluded)
            {
                return new Customers()
                {
                    Address = source.Address,
                    Authority = source.Authority,
                    DateOfBirth = source.DateOfBirth,
                    DateOfIssue = source.DateOfIssue,
                    FIO = source.FIO,
                    IdCustomer = source.IDCUS,
                    PassportNo = source.PassportNo,
                    PhoneNumber = source.PhoneNumber
                };
            }
            else
            {
                return new Customers()
                {
                    Address = source.Address,
                    Authority = source.Authority,
                    DateOfBirth = source.DateOfBirth,
                    DateOfIssue = source.DateOfIssue,
                    FIO = source.FIO,
                    PassportNo = source.PassportNo,
                    PhoneNumber = source.PhoneNumber
                };
            }
        }

        CustomersModel ToObject(Customers source)
        {
            return new CustomersModel()
            {
                Address = source.Address,
                Authority = source.Authority,
                DateOfBirth = source.DateOfBirth,
                DateOfIssue = source.DateOfIssue,
                FIO = source.FIO,
                IDCUS = source.IdCustomer,
                PassportNo = source.PassportNo,
                PhoneNumber = source.PhoneNumber
            };
        }
        public void Add(CustomersModel item, bool isIdIncluded = false)
        {
            var entity = this.ToEntity(item, isIdIncluded);
            caContext.Customers.Add(entity);
            SaveChanges();
        }

        public void Remove(CustomersModel item)
        {
            var entity = this.caContext.Customers.FirstOrDefault(x => x.IdCustomer == item.IDCUS);
            if (entity != null)
            {
                caContext.Customers.Remove(entity);
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }
        public void Remove(int id)
        {
            var entity = this.caContext.Customers.FirstOrDefault(x => x.IdCustomer == id);
            if (entity != null)
            {
                caContext.Customers.Remove(entity);
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public void Update(CustomersModel item)
        {
            var entity = this.caContext.Customers.FirstOrDefault(x => x.IdCustomer == item.IDCUS);
            if (entity != null)
            {
                entity.Address = item.Address;
                entity.Authority = item.Authority;
                entity.DateOfBirth = item.DateOfBirth;
                entity.DateOfIssue = item.DateOfIssue;
                entity.FIO = item.FIO;
                entity.PassportNo = item.PassportNo;
                entity.PhoneNumber = item.PhoneNumber;
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public Customers GetEntity(CustomersModel source)
        {
            var entity = this.caContext.Customers.FirstOrDefault(x => x.IdCustomer == source.IDCUS);
            return entity;
        }

        public IEnumerable<CustomersModel> Items
        {
            get 
            {
                var modelsList = new List<CustomersModel>();
                foreach (var entity in caContext.Customers.Select(x => x))
                {
                    modelsList.Add(ToObject(entity));
                }

                return modelsList;
            }
        }

        public List<Assembly> GetAllAssembliesByCustomerId(int idCustomer)
        {
            var customer = caContext.Customers.Find(idCustomer);
            return customer.Assembly.ToList();
        }

        public List<Selling> GetAllSellingsByCustomerId(int idCustomer)
        {
            var customer = caContext.Customers.Find(idCustomer);
            return customer.Selling.ToList();
        }

        public void SaveChanges()
        {
            caContext.SaveChanges();
        }
    }
}
