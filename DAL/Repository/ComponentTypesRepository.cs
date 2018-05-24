using DAL.DBModel;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ComponentTypesRepository : AbstractRepository, IModelRepository<ComponentTypesModel, ComponentTypes>
    {
        ComponentTypes ToEntity(ComponentTypesModel source)
        {
            return new ComponentTypes()
            {
                ID = source.ID,
                Type = source.Type
            };
        }

        ComponentTypesModel ToObject(ComponentTypes source)
        {
            return new ComponentTypesModel()
            {
                ID = source.ID,
                Type = source.Type
            };
        }
        public void Add(ComponentTypesModel item)
        {
            var entity = this.ToEntity(item);
            caContext.ComponentTypes.Add(entity);
            SaveChanges();
        }

        public void Remove(ComponentTypesModel item)
        {
            var entity = this.caContext.ComponentTypes.FirstOrDefault(x => x.ID == item.ID);
            if (entity != null)
            {
                caContext.ComponentTypes.Remove(entity);
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public void Update(ComponentTypesModel item)
        {
            var entity = this.caContext.ComponentTypes.FirstOrDefault(x => x.ID == item.ID);
            if (entity != null)
            {
                entity.ID = item.ID;
                entity.Type = item.Type;
                SaveChanges();
            }
            else
            {
                throw new ArgumentException("Incorrect argument!!!");
            }
        }

        public ComponentTypes GetEntity(ComponentTypesModel source)
        {
            var entity = this.caContext.ComponentTypes.FirstOrDefault(x => x.ID == source.ID);
            return entity;
        }

        public IEnumerable<ComponentTypesModel> Items
        {
            get 
            {
                var modelsList = new List<ComponentTypesModel>();
                foreach (var entity in caContext.ComponentTypes.Select(x => x))
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
