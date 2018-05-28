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

namespace ComputerAssembly
{
    public partial class sprReceiptOne : Form
    {
        OleDbConnection Con = new OleDbConnection();
        public sprReceiptOne()
        {
            InitializeComponent();
            Con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=DB.mdb;Persist Security Info=False;";
        }

        string typeQuery;
        string idQuery;
        string Mode;

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
            loadReceipts();
        }

        private void loadReceipts()
        {
            try
            {
                Con.Open();
                string qText = "SELECT * FROM Receipts WHERE IDR = @id";
                OleDbCommand Com = new OleDbCommand();
                Com.Parameters.AddWithValue("@id", this.idQuery);
                Com.CommandText = qText;
                OleDbDataReader reader = Com.ExecuteReader();
                string IDSUP = "";
                string IDCOM = "";
                while (reader.Read())
                {
                    IDSUP = getSuppliersById(reader["IDSUP"].ToString());
                    IDCOM = getComponentById(reader["IDCOM"].ToString());
                }
                Con.Close();
                tbFIO.Text = IDSUP;
                tbCom.Text = IDCOM;
                tbQual.Text = reader["Quality"].ToString();
                dtR.Text = reader["ReceiptDate"].ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private string getComponentById(string id)
        {
            string qText = "SELECT c.Nazv FROM Components c WHERE IDCOM = @id";
            OleDbCommand Com = new OleDbCommand(qText, Con);
            Com.Parameters.AddWithValue("@id", id);
            string naim = Com.ExecuteScalar().ToString();
            return naim;
        }

        private string getSuppliersById(string naim)
        {
            string qText = "SELECT c.FIO FROM Suppliers c WHERE c.IDSUP = @id";
            OleDbCommand Com = new OleDbCommand(qText, Con);
            Com.Parameters.AddWithValue("@id", naim);
            string customer = Com.ExecuteScalar().ToString();
            return customer;
        }

        private int getIdComponent(string naim)
        {
            string qText = "SELECT c.IDCOM FROM Components c WHERE c.Nazv = @naim";
            OleDbCommand Com = new OleDbCommand();
            Com.Parameters.AddWithValue("@naim", naim);
            Com.CommandText = qText;
            Com.Connection = Con;
            Con.Open();
            int id = Convert.ToInt32(Com.ExecuteScalar());
            Con.Close();
            return id;
        }

        private int getIdSuppliers(string naim)
        {
            string qText = "SELECT c.IDSUP FROM Suppliers c WHERE c.FIO = @naim";
            OleDbCommand Com = new OleDbCommand();
            Com.Parameters.AddWithValue("@naim", naim);
            Com.CommandText = qText;
            Com.Connection = Con;
            Con.Open();
            int id = (int)Com.ExecuteScalar();
            Con.Close();
            return id;
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
            saveReceipts();
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveReceipts()
        {
            try
            {
                OleDbCommand Com = new OleDbCommand();
                string qText = "";
                if (this.typeQuery == "edit")
                {
                    qText = "UPDATE Receipts SET IDSUP = @Suppliers, IDCOM = @Component, Quality = @Quality, ReceiptDate = @ReceiptDate";
                }
                else
                {
                    qText = "INSERT INTO Receipts (IDSUP, IDCOM, Quality, ReceiptDate) VALUES (@Suppliers, @Component, @Quality, @ReceiptDate)";
                }
                Com.Parameters.AddWithValue("@Suppliers", (int)getIdSuppliers(tbFIO.Text));
                Com.Parameters.AddWithValue("@Component", (int)getIdComponent(tbCom.Text));
                Com.Parameters.AddWithValue("@Quality", Convert.ToInt32(tbQual.Text));
                Com.Parameters.AddWithValue("@ReceiptDate", Convert.ToDateTime(dtR.Text));
                Com.CommandText = qText;
                Com.Connection = Con;
                Con.Open();
                Com.ExecuteNonQuery();
                Con.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            finally
            {
                Con.Close();
            }
        }
    }
}
