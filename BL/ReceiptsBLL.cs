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
    public class ReceiptsBLL
    {
        private IModelRepository<ReceiptsModel, Receipts> receiptsRepository;

        public ReceiptsBLL()
        {
            receiptsRepository = new ReceiptsRepository();
        }

        public List<ReceiptsModel> GetAllReceiptsList()
        {
            var customers = new List<ReceiptsModel>();
            customers.AddRange(receiptsRepository.Items);
            return customers;
        }
        public Task<List<ReceiptsModel>> GetAllReceiptsListAsync()
        {
            return Task<List<ReceiptsModel>>.Factory.StartNew(() => GetAllReceiptsList());
        }

        public ReceiptsModel FindReceiptById(int idReceipt)
        {
            return receiptsRepository.Items.FirstOrDefault(x => x.IDR == idReceipt);
        }

        public void Remove(int idReceipt)
        {
            receiptsRepository.Remove(idReceipt);
        }

        public void AddOrUpdateReceipt(int idReceipt, int idComponent, int idSupplier,
            decimal price, int count, string date)
        {
            var receiptDate = DateTime.Today;
            var isDate = DateTime.TryParse(date, out receiptDate);
            var receipt = new ReceiptsModel();
            receipt.IDCOM = idComponent;
            receipt.IDR = idReceipt;
            receipt.IDSUP = idSupplier;
            receipt.Price = price;
            receipt.Quality = count;
            receipt.ReceiptDate = receiptDate;

            var flag = receiptsRepository.Items.Any(x => x.IDR == receipt.IDR);
            if (flag)
            {
                receiptsRepository.Update(receipt);
            }
            else
            {
                receiptsRepository.Add(receipt, true);
            }
        }
    }
}
