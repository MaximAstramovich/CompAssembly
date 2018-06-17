using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using DAL.Models;

namespace ComputerAssembly
{
    public partial class sprReceiptOne : BaseForm
    {
        public sprReceiptOne()
        {
            InitializeComponent();
        }

        string typeQuery;
        string idQuery;
        string Mode;
        int _idReceipt = 0;
        SuppliersModel _currentSupplier;
        public SuppliersModel SetCurrentSupplier 
        { 
            set 
            {
                if (value != null && value != _currentSupplier)
                {
                    _currentSupplier = value;
                }
            } 
        }
        ComponentsModel _currentComponent;
        public ComponentsModel SetCurrentComponent
        {
            set
            {
                if (value != null && value != _currentComponent)
                {
                    _currentComponent = value;
                }
            } 
        }
        public int IdReceipt
        {
            get { return _idReceipt; }
            set { _idReceipt = value; }
        }

        public string Suppliers
        {
            set { tbFIO.Text = value; }
            get { return tbFIO.Text; }
        }

        public string Accessory
        {
            set { tbCom.Text = value; }
            get { return tbCom.Text; }
        }

        public string id
        {
            set { idQuery = value; }
            get { return idQuery; }
        }

        public string readMode
        {
            set { Mode = value; }
        }

        public string type
        {
            set
            {
                typeQuery = value;
            }
        }

        private void sprReceiptOne_Load(object sender, EventArgs e)
        {
            if (_idReceipt > 0)
            {
                loadReceipt(_idReceipt);
            }
        }

        private void loadReceipt(int idReceipt)
        {
            try
            {
                var receipt = ReceiptsBusinessLayer.FindReceiptById(idReceipt);
                _currentComponent = receipt.Component;
                _currentSupplier = receipt.Supplier;
                if (receipt != null)
                {
                    tbFIO.Text = receipt.Supplier.FIO;
                    tbCom.Text = receipt.Component.Nazv;
                    tbQual.Text = receipt.Quality.ToString();
                    dtR.Text = receipt.ReceiptDate.ToString();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public void UpdateSupplierFioField()
        {
            tbFIO.Text = _currentSupplier.FIO;
            Text = _currentSupplier.Firm;
        }

        public void UpdateComponentField()
        {
            tbCom.Text = _currentComponent.Nazv;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sprSuppliersList sprSuppliersListForm = new sprSuppliersList();
            sprSuppliersListForm.ModeCheck = "1";
            sprSuppliersListForm.Owner = this;
            sprSuppliersListForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sprAccessoryList sprAccessoryListForm = new sprAccessoryList();
            sprAccessoryListForm.ModeCheck = "1";
            sprAccessoryListForm.Owner = this;
            sprAccessoryListForm.ShowDialog();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = 0;
            var flag = int.TryParse(tbQual.Text, out count);
            if (_idReceipt > 0)
            {

                ReceiptsBusinessLayer.AddOrUpdateReceipt(_idReceipt, _currentComponent.IDCOM, 
                    _currentSupplier.IdSuppliers, (decimal)_currentComponent.Price, count, dtR.Text);
            }
            else
            {
                count = (int)_currentComponent.Stock.InStock + count;
                ReceiptsBusinessLayer.AddOrUpdateReceipt(_idReceipt, _currentComponent.IDCOM,
                    _currentSupplier.IdSuppliers, (decimal)_currentComponent.Price, count, dtR.Text);
            }
            this.Close();
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
