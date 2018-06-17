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
    public class AssemblyBLL
    {
        private IModelRepository<AssemblyModel, Assembly> assemblyRepository;
        private IModelRepository<ComponentTypesModel, ComponentTypes> componentTypesRepository;
        private IModelRepository<ComponentsModel, Components> componentsRepository;
        private IModelRepository<StockModel, Stock> stockRepository;
        private IModelRepository<SellingModel, Selling> sellingRepository;

        public AssemblyBLL()
        {
            assemblyRepository = new AssemblyRepository();
            componentTypesRepository = new ComponentTypesRepository();
            componentsRepository = new ComponentsRepository();
            stockRepository = new StockRepository();
            sellingRepository = new SellingRepository();
        }

        public List<AssemblyModel> GetAllAssemblyList()
        {
            var assemblyList = new List<AssemblyModel>();
            assemblyList.AddRange(assemblyRepository.Items);
            return assemblyList;
        }
        public Task<List<AssemblyModel>> GetAllAssemblyListAsync()
        {
            return Task<List<AssemblyModel>>.Factory.StartNew(() => GetAllAssemblyList());
        }

        public List<StockModel> GetStockList()
        {
            var list = new List<StockModel>();
            list.AddRange(stockRepository.Items);
            return list;
        }

        public List<ComponentTypesModel> GetAllComponentTypes()
        {
            var list = new List<ComponentTypesModel>();
            list.AddRange(componentTypesRepository.Items);
            return list;
        }

        public Task<List<ComponentTypesModel>> GetAllComponentTypesAsync()
        {
            return Task<List<ComponentTypesModel>>.Factory.StartNew(() => GetAllComponentTypes());
        }

        public List<ComponentsModel> GetAllComponentsList()
        {
            var components = new List<ComponentsModel>();
            components.AddRange(componentsRepository.Items);
            return components;
        }
        public Task<List<ComponentsModel>> GetAllComponentsListAsync()
        {
            return Task<List<ComponentsModel>>.Factory.StartNew(() => GetAllComponentsList());
        }

        public AssemblyModel FindAssemblyById(int idAssembly)
        {
            return assemblyRepository.Items.FirstOrDefault(x => x.IdAssembly == idAssembly);
        }

        public void Remove(int idAssembly)
        {
            assemblyRepository.Remove(idAssembly);
        }

        public void AddSell(int idSelling, int? idCom, int? idCustomer, decimal? price, int? quality, string dateOfSale)
        {
            var sell = new SellingModel()
            {
                //IDS = idSelling,
                IDCOM = idCom,
                IDCUS = idCustomer,
                Price = price,
                Quality = quality,
                DateOfSale = dateOfSale
            };
            sellingRepository.Add(sell);
        }

        public void UpdateStock(int id, int newValue)
        {
            var stockItem = new StockModel
            {
                IdStock = id,
                InStock = newValue
            };
            stockRepository.Update(stockItem);
        }

        public void AddOrUpdateAssembly(int idAssembly, int audio, int board, int corpus,
            int cpu, DateTime? dateOfPayment, int dvd, int graphic, int hdd, int ice, int idCus,
            int num, DateTime orderDate, int ozu, int power, int ssd, int status, decimal summ)
        {
            var assembly = new AssemblyModel();
            assembly.Audio = audio;
                assembly.Board = board;
                assembly.Corpus = corpus;
                assembly.CPU = cpu;
                assembly.DateOfPayment = dateOfPayment;
                assembly.DVD = dvd;
                assembly.Graphic = graphic;
                assembly.HDD = hdd;
                assembly.Ice = ice;
                assembly.IDCUS = idCus;
                assembly.Num = num;
                assembly.OrderDate = orderDate;
                assembly.OZU = ozu;
                assembly.Power = power;
                assembly.SSD = ssd;
                assembly.Status = status;
                assembly.Summ = summ;
                assembly.IdAssembly = idAssembly;

            var flag = assemblyRepository.Items.Any(x => x.IdAssembly == assembly.IdAssembly);
            if (flag)
            {
                assemblyRepository.Update(assembly);
            }
            else
            {
                assemblyRepository.Add(assembly, true);
            }
        }
    }
}
