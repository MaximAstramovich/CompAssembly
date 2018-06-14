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
                IdComponentType = source.ID,
                Type = source.Type
            };
        }

        ComponentTypesModel ToObject(ComponentTypes source)
        {
            return new ComponentTypesModel()
            {
                ID = source.IdComponentType,
                Type = source.Type
            };
        }
        public void Add(ComponentTypesModel item, bool isIdIncluded = false)
        {
            var entity = this.ToEntity(item);
            caContext.ComponentTypes.Add(entity);
            SaveChanges();
        }

        public void Remove(ComponentTypesModel item)
        {
            var entity = this.caContext.ComponentTypes.FirstOrDefault(x => x.IdComponentType == item.ID);
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

        public void Remove(int id)
        {
            var entity = this.caContext.ComponentTypes.FirstOrDefault(x => x.IdComponentType == id);
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
            var entity = this.caContext.ComponentTypes.FirstOrDefault(x => x.IdComponentType == item.ID);
            if (entity != null)
            {
                //entity.IdComponentType = item.ID;
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
            var entity = this.caContext.ComponentTypes.FirstOrDefault(x => x.IdComponentType == source.ID);
            return entity;
        }

        public IEnumerable<ComponentTypesModel> Items
        {
            get 
            {
                var modelsList = new List<ComponentTypesModel>();
                foreach (var entity in caContext.ComponentTypes.Select(x => x))
                {
                    var component = ToObject(entity);
                    component.Components = GetAllComponentsByComponentTypeId(component.ID);
                    modelsList.Add(component);
                }

                return modelsList;
            }
        }

        public List<ComponentsModel> GetAllComponentsByComponentTypeId(int idComponentType)
        {
            var componentTypes = caContext.ComponentTypes.Find(idComponentType).Components;
            List<ComponentsModel> componentsList = new List<ComponentsModel>();
            foreach (var item in componentTypes)
            {
                var component = new ComponentsModel()
                {
                    Description = item.Description,
                    IDCOM = item.IdCom,
                    Nazv = item.Nazv,
                    Price = item.Price,
                    Type = item.Type
                };
                componentsList.Add(component);
            }
            return componentsList;
        }

        public void SaveChanges()
        {
            caContext.SaveChanges();
        }
    }
}
