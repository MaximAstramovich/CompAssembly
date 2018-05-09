using DAL.DBModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    class ReceiptsRepository : AbstractRepository, IModelRepository<ReceiptsModel, Receipts>
    {
        Receipts ToEntity(ReceiptsModel source)
        {
            return new Receipts()
            {
                 IDR = source.IDR,
                 IDCOM = source.IDCOM,
                 IDSUP = source.IDSUP,
                 Price = source.Price,
                 Quality = source.Quality,
                 ReceiptDate = source.ReceiptDate
            };
        }

        ReceiptsModel ToObject(Receipts source)
        {
            return new ReceiptsModel()
            {
                IDR = source.IDR,
                IDCOM = source.IDCOM,
                IDSUP = source.IDSUP,
                Price = source.Price,
                Quality = source.Quality,
                ReceiptDate = source.ReceiptDate
            };
        }
        public void Add(ReceiptsModel item)
        {
            var entity = this.ToEntity(item);
            caContext.Receipts.Add(entity);
            SaveChanges();
        }

        public void Remove(ReceiptsModel item)
        {
            var entity = this.caContext.Receipts.FirstOrDefault(x => x.IDR == item.IDR);
            if (entity != null)
            {
                caContext.Receipts.Remove(entity);
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public void Update(ReceiptsModel item)
        {
            var entity = this.caContext.Receipts.FirstOrDefault(x => x.IDR == item.IDR);
            if (entity != null)
            {
                entity.IDR = item.IDR;
                entity.IDCOM = item.IDCOM;
                entity.IDSUP = item.IDSUP;
                entity.Price = item.Price;
                entity.Quality = item.Quality;
                entity.ReceiptDate = item.ReceiptDate;
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public Receipts GetEntity(ReceiptsModel source)
        {
            var entity = this.caContext.Receipts.FirstOrDefault(x => x.IDR == source.IDR);
            return entity;
        }

        public IEnumerable<ReceiptsModel> Items
        {
            get
            {
                var modelsList = new List<ReceiptsModel>();
                foreach (var entity in caContext.Receipts.Select(x => x))
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
