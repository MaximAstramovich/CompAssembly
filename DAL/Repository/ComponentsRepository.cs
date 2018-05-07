using DAL.DBModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    class ComponentsRepository : AbstractRepository, IModelRepository<ComponentsModel, Components>
    {
        Components ToEntity(ComponentsModel source)
        {
            return new Components()
            {
                Description = source.Description,
                IDCOM = source.IDCOM,
                Nazv = source.Nazv,
                Price = source.Price,
                Type = source.Type
            };
        }

        ComponentsModel ToObject(Components source)
        {
            return new ComponentsModel()
            {
                Description = source.Description,
                IDCOM = source.IDCOM,
                Nazv = source.Nazv,
                Price = source.Price,
                Type = source.Type
            };
        }
        public void Add(ComponentsModel item)
        {
            var entity = this.ToEntity(item);
            caContext.Components.Add(entity);
            SaveChanges();
        }

        public void Remove(ComponentsModel item)
        {
            var entity = this.caContext.Components.FirstOrDefault(x => x.IDCOM == item.IDCOM);
            if (entity != null)
            {
                caContext.Components.Remove(entity);
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public void Update(ComponentsModel item)
        {
            var entity = this.caContext.Components.FirstOrDefault(x => x.IDCOM == item.IDCOM);
            if (entity != null)
            {
                entity.Description = item.Description;
                entity.IDCOM = item.IDCOM;
                entity.Nazv = item.Nazv;
                entity.Price = item.Price;
                entity.Type = item.Type;
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public Components GetEntity(ComponentsModel source)
        {
            var entity = this.caContext.Components.FirstOrDefault(x => x.IDCOM == source.IDCOM);
            return entity;
        }

        public IEnumerable<ComponentsModel> Items
        {
            get
            {
                var modelsList = new List<ComponentsModel>();
                foreach (var entity in caContext.Components.Select(x => x))
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
