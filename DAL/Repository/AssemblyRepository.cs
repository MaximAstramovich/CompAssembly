using DAL.DBModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    class AssemblyRepository : AbstractRepository, IModelRepository<AssemblyModel, Assembly>
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
                IDCUS = source.IDCUS,
                Num = source.Num,
                OrderDate = source.OrderDate,
                OZU = source.OZU,
                Power = source.Power,
                SSD = source.SSD,
                Status = source.Status,
                Summ = source.Summ,
                Код = source.Code
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
                IDCUS = source.IDCUS,
                Num = source.Num,
                OrderDate = source.OrderDate,
                OZU = source.OZU,
                Power = source.Power,
                SSD = source.SSD,
                Status = source.Status,
                Summ = source.Summ,
                Code = source.Код
            };
        }
        public void Add(AssemblyModel item)
        {
            var entity = this.ToEntity(item);
            caContext.Assembly.Add(entity);
            SaveChanges();
        }

        public void Remove(AssemblyModel item)
        {
            var entity = this.caContext.Assembly.FirstOrDefault(x => x.IDCUS == item.IDCUS);
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
            var entity = this.caContext.Assembly.FirstOrDefault(x => x.IDCUS == item.IDCUS);
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
                entity.IDCUS = item.IDCUS;
                entity.Num = item.Num;
                entity.OrderDate = item.OrderDate;
                entity.OZU = item.OZU;
                entity.Power = item.Power;
                entity.SSD = item.SSD;
                entity.Status = item.Status;
                entity.Summ = item.Summ;
                entity.Код = item.Code;
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public Assembly GetEntity(AssemblyModel source)
        {
            var entity = this.caContext.Assembly.FirstOrDefault(x => x.IDCUS == source.IDCUS);
            return entity;
        }

        public IEnumerable<AssemblyModel> Items
        {
            get 
            {
                var modelsList = new List<AssemblyModel>();
                foreach (var entity in caContext.Assembly.Select(x => x))
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
