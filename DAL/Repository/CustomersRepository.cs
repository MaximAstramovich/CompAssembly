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
                    var customer = ToObject(entity);
                    customer.Assembly = GetAllAssembliesByCustomerId(customer.IDCUS);
                    customer.Selling = GetAllSellingsByCustomerId(customer.IDCUS);
                    modelsList.Add(customer);
                }

                return modelsList;
            }
        }

        public List<AssemblyModel> GetAllAssembliesByCustomerId(int idCustomer)
        {
            var source = caContext.Customers.Find(idCustomer).Assembly.Where(x => x.IdCus == idCustomer);
            List<AssemblyModel> assemblyList = new List<AssemblyModel>();
            foreach (var item in source)
            {
                var assembly = new AssemblyModel()
                {
                    Audio = item.Audio,
                    Board = item.Board,
                    Corpus = item.Corpus,
                    CPU = item.CPU,
                    DateOfPayment = item.DateOfPayment,
                    DVD = item.DVD,
                    Graphic = item.Graphic,
                    HDD = item.HDD,
                    Ice = item.Ice,
                    IDCUS = item.IdCus,
                    Num = item.Num,
                    OrderDate = item.OrderDate,
                    OZU = item.OZU,
                    Power = item.Power,
                    SSD = item.SSD,
                    Status = item.Status,
                    Summ = item.Summ,
                    IdAssembly = item.IdAssembly
                };
                assemblyList.Add(assembly);
            }
            return assemblyList;
        }

        public List<SellingModel> GetAllSellingsByCustomerId(int idCustomer)
        {
            var source = caContext.Customers.Find(idCustomer).Selling.Where(x => x.IdCustomer == idCustomer);
            var sellingList = new List<SellingModel>();
            foreach (var item in source)
            {
                var sell = new SellingModel()
                {
                    IDS = item.IdSelling,
                    IDCOM = item.IdCom,
                    IDCUS = item.IdCustomer,
                    Price = item.Price,
                    Quality = item.Quality,
                    DateOfSale = item.DateOfSale
                };
                sellingList.Add(sell);
            }
            return sellingList;
        }

        public void SaveChanges()
        {
            caContext.SaveChanges();
        }
    }
}
