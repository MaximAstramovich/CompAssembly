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

        public void AddOrUpdateCustomer(string address, string authority, DateTime? dateOfBirth, 
                                DateTime? dateOfIssue, string fio, //int idCus, 
                                string passportNo, int? phoneNumber)
        {
            var customer = new CustomersModel();
            customer.Address = address;
            customer.Authority = authority;
            customer.DateOfBirth = dateOfBirth;
            customer.DateOfIssue = dateOfIssue;
            customer.FIO = fio;
            //customer.IDCUS = idCus;
            customer.PassportNo = passportNo;
            customer.PhoneNumber = phoneNumber;

            var flag = customersRepository.Items.Contains(customer);
            if (flag)
            {
                customersRepository.Update(customer);
            }
            else
            {
                customersRepository.Add(customer);
            }
        }
    }
}
