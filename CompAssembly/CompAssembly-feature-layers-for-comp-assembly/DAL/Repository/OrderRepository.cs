using DAL.DBModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class OrderRepository : AbstractRepository, IModelRepository<OrderModel, Order>
    {
        Order ToEntity(OrderModel source)
        {
            return new Order()
            {
                DateOfPayment = source.DateOfPayment,
                NumberAssembly = source.NumberAssembly,
                Код = source.IdOrder
            };
        }

        OrderModel ToObject(Order source)
        {
            return new OrderModel()
            {
                DateOfPayment = source.DateOfPayment,
                NumberAssembly = source.NumberAssembly,
                IdOrder = source.Код
            };
        }
        public void Add(OrderModel item)
        {
            var entity = this.ToEntity(item);
            caContext.Order.Add(entity);
            SaveChanges();
        }

        public void Remove(OrderModel item)
        {
            var entity = this.caContext.Order.FirstOrDefault(x => x.Код == item.IdOrder);
            if (entity != null)
            {
                caContext.Order.Remove(entity);
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public void Update(OrderModel item)
        {
            var entity = this.caContext.Order.FirstOrDefault(x => x.Код == item.IdOrder);
            if (entity != null)
            {
                entity.DateOfPayment = item.DateOfPayment;
                entity.NumberAssembly = item.NumberAssembly;
                entity.Код = item.IdOrder;
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public Order GetEntity(OrderModel source)
        {
            var entity = this.caContext.Order.FirstOrDefault(x => x.Код == source.IdOrder);
            return entity;
        }

        public IEnumerable<OrderModel> Items
        {
            get
            {
                var modelsList = new List<OrderModel>();
                foreach (var entity in caContext.Order.Select(x => x))
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
