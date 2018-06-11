﻿using DAL.DBModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class StockRepository : AbstractRepository, IModelRepository<StockModel, Stock>
    {
        Stock ToEntity(StockModel source)
        {
            return new Stock()
            {
                IdCom = source.IDCOM,
                InStock = source.InStock
            };
        }

        StockModel ToObject(Stock source)
        {
            return new StockModel()
            {
                IDCOM = source.IdCom,
                InStock = source.InStock
            };
        }
        public void Add(StockModel item, bool isIdIncluded = false)
        {
            var entity = this.ToEntity(item);
            caContext.Stock.Add(entity);
            SaveChanges();
        }

        public void Remove(StockModel item)
        {
            var entity = this.caContext.Stock.FirstOrDefault(x => x.IdCom == item.IDCOM);
            if (entity != null)
            {
                caContext.Stock.Remove(entity);
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public void Remove(int id)
        {
            var entity = this.caContext.Stock.FirstOrDefault(x => x.IdCom == id);
            if (entity != null)
            {
                caContext.Stock.Remove(entity);
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public void Update(StockModel item)
        {
            var entity = this.caContext.Stock.FirstOrDefault(x => x.IdCom == item.IDCOM);
            if (entity != null)
            {
                //entity.IdCom = item.IDCOM;
                entity.InStock = item.InStock;
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public Stock GetEntity(StockModel source)
        {
            var entity = this.caContext.Stock.FirstOrDefault(x => x.IdCom == source.IDCOM);
            return entity;
        }

        public IEnumerable<StockModel> Items
        {
            get
            {
                var modelsList = new List<StockModel>();
                foreach (var entity in caContext.Stock.Select(x => x))
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
