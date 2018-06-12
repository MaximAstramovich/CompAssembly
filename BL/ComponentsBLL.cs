using DAL.DBModel;
using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ComponentsBLL
    {
        private IModelRepository<ComponentsModel, Components> componentsRepository;

        public ComponentsBLL()
        {
            componentsRepository = new ComponentsRepository();
        }

        public List<ComponentsModel> GetAllComponentsList()
        {
            var components = new List<ComponentsModel>();
            components.AddRange(componentsRepository.Items);
            return components;
        }
        public Task<List<ComponentsModel>> GetAllReceiptsListAsync()
        {
            return Task<List<ComponentsModel>>.Factory.StartNew(() => GetAllComponentsList());
        }

        public ComponentsModel FindComponentById(int idComponent)
        {
            return componentsRepository.Items.FirstOrDefault(x => x.IDCOM == idComponent);
        }

        public void Remove(int idComponent)
        {
            componentsRepository.Remove(idComponent);
        }

        public void AddOrUpdateComponent(int idComponent, string description,
            decimal price, int idType, string nazv)
        {
            var component = new ComponentsModel();
            component.IDCOM = idComponent;
            component.Description = description;
            component.Nazv = nazv;
            component.Price = price;
            component.Type = idType;

            var flag = componentsRepository.Items.Any(x => x.IDCOM == component.IDCOM);
            if (flag)
            {
                componentsRepository.Update(component);
            }
            else
            {
                componentsRepository.Add(component, true);
            }
        }
    }
}
