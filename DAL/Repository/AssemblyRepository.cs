using DAL.DBModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class AssemblyRepository : AbstractRepository, IModelRepository<AssemblyModel, Assembly>
    {
        Assembly ToEntity(AssemblyModel source)
        {
            return new Assembly()
            {
                Audio = source.Audio,
                Board = source.Board,
                Corpus = source.Corpus, 
                CPU = source.CPU,
                DateOfPayment = source.DateOfPayment,
                DVD = source.DVD,
                Graphic = source.Graphic,
                HDD = source.HDD,
                Ice = source.Ice,
                IdCus = source.IDCUS,
                Num = source.Num,
                OrderDate = source.OrderDate,
                OZU = source.OZU,
                Power = source.Power,
                SSD = source.SSD,
                Status = source.Status,
                Summ = source.Summ, 
                IdAssembly = source.IdAssembly
            };
        }

        AssemblyModel ToObject(Assembly source)
        {
            return new AssemblyModel()
            {
                Audio = source.Audio,
                Board = source.Board,
                Corpus = source.Corpus,
                CPU = source.CPU,
                DateOfPayment = source.DateOfPayment,
                DVD = source.DVD,
                Graphic = source.Graphic,
                HDD = source.HDD,
                Ice = source.Ice,
                IDCUS = source.IdCus,
                Num = source.Num,
                OrderDate = source.OrderDate,
                OZU = source.OZU,
                Power = source.Power,
                SSD = source.SSD,
                Status = source.Status,
                Summ = source.Summ,
                IdAssembly = source.IdAssembly
            };
        }
        public void Add(AssemblyModel item, bool isIdIncluded = false)
        {
            var entity = this.ToEntity(item);
            caContext.Assembly.Add(entity);
            SaveChanges();
        }

        public void Remove(AssemblyModel item)
        {
            var entity = this.caContext.Assembly.FirstOrDefault(x => x.IdAssembly == item.IdAssembly);
            if (entity != null)
            {
                caContext.Assembly.Remove(entity);
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public void Remove(int id)
        {
            var entity = this.caContext.Assembly.FirstOrDefault(x => x.IdAssembly == id);
            if (entity != null)
            {
                caContext.Assembly.Remove(entity);
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public void Update(AssemblyModel item)
        {
            var entity = this.caContext.Assembly.FirstOrDefault(x => x.IdAssembly == item.IdAssembly);
            if (entity != null)
            {
                entity.Audio = item.Audio;
                entity.Board = item.Board;
                entity.Corpus = item.Corpus;
                entity.CPU = item.CPU;
                entity.DateOfPayment = item.DateOfPayment;
                entity.DVD = item.DVD;
                entity.Graphic = item.Graphic;
                entity.HDD = item.HDD;
                entity.Ice = item.Ice;
                entity.IdCus = item.IDCUS;
                entity.Num = item.Num;
                entity.OrderDate = item.OrderDate;
                entity.OZU = item.OZU;
                entity.Power = item.Power;
                entity.SSD = item.SSD;
                entity.Status = item.Status;
                entity.Summ = item.Summ;
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public Assembly GetEntity(AssemblyModel source)
        {
            var entity = this.caContext.Assembly.FirstOrDefault(x => x.IdAssembly == source.IdAssembly);
            return entity;
        }

        public IEnumerable<AssemblyModel> Items
        {
            get 
            {
                var modelsList = new List<AssemblyModel>();
                foreach (var entity in caContext.Assembly.Select(x => x))
                {
                    var assembly = ToObject(entity);
                    assembly.Customers = GetCustomerByAssemblyId(assembly.IdAssembly);
                    modelsList.Add(assembly);
                }

                return modelsList;
            }
        }

        public CustomersModel GetCustomerByAssemblyId(int idAssembly)
        {
            var source = caContext.Assembly.Find(idAssembly).Customers;
            var customer = new CustomersModel()
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
            return customer;
        }

        public void SaveChanges()
        {
            caContext.SaveChanges();
        }
    }
}
