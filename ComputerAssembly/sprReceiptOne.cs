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
        //OleDbConnection Con = new OleDbConnection();
        public sprReceiptOne()
        {
            InitializeComponent();
            //Con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=DB.mdb;Persist Security Info=False;";
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
                //Con.Open();
                //string qText = "SELECT * FROM Receipts WHERE IDR = @id";
                //OleDbCommand Com = new OleDbCommand();
                //Com.Parameters.AddWithValue("@id", this.idQuery);
                //Com.CommandText = qText;
                //OleDbDataReader reader = Com.ExecuteReader();
                //string IDSUP = "";
                //string IDCOM = "";
                //while (reader.Read())
                //{
                //    IDSUP = getSuppliersById(reader["IDSUP"].ToString());
                //    IDCOM = getComponentById(reader["IDCOM"].ToString());
                //}
                //Con.Close();
                //tbFIO.Text = IDSUP;
                //tbCom.Text = IDCOM;
                //tbQual.Text = reader["Quality"].ToString();
                //dtR.Text = reader["ReceiptDate"].ToString();
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
            //finally
            //{
            //    Con.Close();
            //}
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

        //private string getComponentById(string id)
        //{
        //    string qText = "SELECT c.Nazv FROM Components c WHERE IDCOM = @id";
        //    OleDbCommand Com = new OleDbCommand(qText, Con);
        //    Com.Parameters.AddWithValue("@id", id);
        //    string naim = Com.ExecuteScalar().ToString();
        //    return naim;
        //}

        //private string getSuppliersById(string naim)
        //{
        //    //string qText = "SELECT c.FIO FROM Suppliers c WHERE c.IDSUP = @id";
        //    //OleDbCommand Com = new OleDbCommand(qText, Con);
        //    //Com.Parameters.AddWithValue("@id", naim);
        //    //string customer = Com.ExecuteScalar().ToString();
        //    //return customer;
        //}

        //private int getIdComponent(string naim)
        //{
        //    //string qText = "SELECT c.IDCOM FROM Components c WHERE c.Nazv = @naim";
        //    //OleDbCommand Com = new OleDbCommand();
        //    //Com.Parameters.AddWithValue("@naim", naim);
        //    //Com.CommandText = qText;
        //    //Com.Connection = Con;
        //    //Con.Open();
        //    //int id = Convert.ToInt32(Com.ExecuteScalar());
        //    //Con.Close();
        //    //return id;
        //}

        //private int getIdSuppliers(string naim)
        //{
        //    string qText = "SELECT c.IDSUP FROM Suppliers c WHERE c.FIO = @naim";
        //    OleDbCommand Com = new OleDbCommand();
        //    Com.Parameters.AddWithValue("@naim", naim);
        //    Com.CommandText = qText;
        //    Com.Connection = Con;
        //    Con.Open();
        //    int id = (int)Com.ExecuteScalar();
        //    Con.Close();
        //    return id;
        //}

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
