using DAL.DBModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ReceiptsRepository : AbstractRepository, IModelRepository<ReceiptsModel, Receipts>
    {
        Receipts ToEntity(ReceiptsModel source)
        {
            return new Receipts()
            {
                 IdReceipts = source.IDR,
                 IdCom = source.IDCOM,
                 IdSuppliers = source.IDSUP,
                 Price = source.Price,
                 Quality = source.Quality,
                 ReceiptDate = source.ReceiptDate
            };
        }

        ReceiptsModel ToObject(Receipts source)
        {
            return new ReceiptsModel()
            {
                IDR = source.IdReceipts,
                IDCOM = source.IdCom,
                IDSUP = source.IdSuppliers,
                Price = source.Price,
                Quality = source.Quality,
                ReceiptDate = source.ReceiptDate
            };
        }
        public void Add(ReceiptsModel item, bool isIdIncluded = false)
        {
            var entity = this.ToEntity(item);
            var stockEntity = caContext.Components.Find(item.IDCOM).Stock;
            caContext.Receipts.Add(entity);
            if (stockEntity != null)
            {
                stockEntity.InStock = item.Quality;
            }
            SaveChanges();
        }

        public void Remove(ReceiptsModel item)
        {
            var entity = this.caContext.Receipts.FirstOrDefault(x => x.IdReceipts == item.IDR);
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

        public void Remove(int id)
        {
            var entity = this.caContext.Receipts.FirstOrDefault(x => x.IdReceipts == id);
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
            var entity = this.caContext.Receipts.FirstOrDefault(x => x.IdReceipts == item.IDR);
            var stockEntity = caContext.Receipts.Find(item.IDR).Components.Stock;
            if (entity != null)
            {
                //entity.IdReceipts = item.IDR;
                entity.IdCom = item.IDCOM;
                entity.IdSuppliers = item.IDSUP;
                entity.Price = item.Price;
                entity.Quality = item.Quality;
                if (stockEntity != null)
                {
                    stockEntity.InStock = item.Quality;
                }
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
            var entity = this.caContext.Receipts.FirstOrDefault(x => x.IdReceipts == source.IDR);
            return entity;
        }

        public IEnumerable<ReceiptsModel> Items
        {
            get
            {
                var modelsList = new List<ReceiptsModel>();
                foreach (var entity in caContext.Receipts.Select(x => x))
                {
                    var receipt = ToObject(entity);
                    receipt.Supplier = GetSupplierByReceiptId(receipt.IDR);
                    receipt.Component = GetComponentByReceiptId(receipt.IDR);
                    receipt.Quality = receipt.Component.Stock.InStock;
                    modelsList.Add(receipt);
                }

                return modelsList;
            }
        }

        public ComponentsModel GetComponentByReceiptId(int idReceipt)
        {
            var component = caContext.Receipts.Find(idReceipt).Components;
            var stock = caContext.Receipts.Find(idReceipt).Components.Stock;
            return new ComponentsModel()
            {
                Description = component.Description,
                IDCOM = component.IdCom,
                Nazv = component.Nazv,
                Price = component.Price,
                Type = component.Type,
                Stock = new StockModel
                {
                    IDCOM = stock.IdCom,
                    InStock = stock.InStock
                }
            };
        }

        public SuppliersModel GetSupplierByReceiptId(int idReceipt)
        {
            var supplier = caContext.Receipts.Find(idReceipt).Suppliers;
            return new SuppliersModel()
            {
                Address = supplier.Address,
                BankCode = supplier.BankCode,
                CheckingAccount = supplier.CheckingAccount,
                FIO = supplier.FIO,
                Firm = supplier.Firm,
                IdSuppliers = supplier.IdSuppliers,
                PhoneNumber = supplier.PhoneNumber,
                Position = supplier.Position,
                UNN = supplier.UNN
            };
        }

        public void SaveChanges()
        {
            caContext.SaveChanges();
        }
    }
}
