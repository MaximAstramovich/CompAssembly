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
    public class CustomersBLL
    {
        private IModelRepository<CustomersModel, Customers> customersRepository;

        public CustomersBLL()
        {
            customersRepository = new CustomersRepository();
        }

        public List<CustomersModel> GetAllCustomersList()
        {
            var customers = new List<CustomersModel>();
            customers.AddRange(customersRepository.Items);
            return customers;
        }
        public Task<List<CustomersModel>> GetAllCustomersListAsync()
        {
            var customers = new List<CustomersModel>();
            customers.AddRange(customersRepository.Items);
            return Task.Factory.StartNew( () => customers);
        }

        public CustomersModel FindCustomerById(int idCustomer)
        {
            return customersRepository.Items.FirstOrDefault(x => x.IDCUS == idCustomer);
        }

        public void Remove(int idCustomer)
        {
            customersRepository.Remove(idCustomer);
        }

        public void AddOrUpdateCustomer(int idCustomer, string address, string authority, DateTime? dateOfBirth, 
                                DateTime? dateOfIssue, string fio, 
                                string passportNo, int? phoneNumber)
        {
            var customer = new CustomersModel();
            customer.Address = address;
            customer.Authority = authority;
            customer.DateOfBirth = dateOfBirth;
            customer.DateOfIssue = dateOfIssue;
            customer.FIO = fio;
            customer.PassportNo = passportNo;
            customer.PhoneNumber = phoneNumber;
            customer.IDCUS = idCustomer;

            var flag = customersRepository.Items.Any(x => x.IDCUS == customer.IDCUS);
            if (flag)
            {
                customersRepository.Update(customer);
            }
            else
            {
                customersRepository.Add(customer, true);
            }
        }
    }
}
