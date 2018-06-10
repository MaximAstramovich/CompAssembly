using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DBModel;
using DAL.Models;
using DAL.Repository;

namespace BL
{
    public class SuppliersBLL
    {
        private IModelRepository<SuppliersModel, Suppliers> suppliersRepository;

        public SuppliersBLL()
        {
            suppliersRepository = new SuppliersRepository();
        }

        public List<SuppliersModel> GetAllSuppliersList()
        {
            var customers = new List<SuppliersModel>();
            customers.AddRange(suppliersRepository.Items);
            return customers;
        }

        public void AddOrUpdateSupplier(string address, string bankCode, int checkingAccount, 
                                        string fio, string firm, int? phoneNumber, 
                                        string position, int unn)
        {
            var supplier = new SuppliersModel();
            supplier.Address = address;
            supplier.BankCode = bankCode;
            supplier.CheckingAccount = checkingAccount;
            supplier.FIO = fio;
            supplier.Firm = firm;
            supplier.PhoneNumber = phoneNumber;
            supplier.Position = position;
            supplier.UNN = unn;

            var flag = suppliersRepository.Items.Contains(supplier);
            if (flag)
            {
                suppliersRepository.Update(supplier);
            }
            else
            {
                suppliersRepository.Add(supplier);
            }
        }
    }
}
