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
    public partial class sprCustomerOne : Form
    {
        OleDbConnection Con = new OleDbConnection();
        
        string typeQuery;
        string idQuery;
        string Mode;

        public string id
        {
            set { idQuery = value; }
            get { return idQuery; }
        }

        public string readMode {
            set { Mode = value; }
        }

        public string type
        {
            set
            {
                typeQuery = value;
            }
        }
        public sprCustomerOne()
        {
            InitializeComponent();
            Con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=DB.mdb;Persist Security Info=False;";
        }

        private bool validate() {

            if (tbFIO.Text != "" || tbAddress.Text != "" || tbAuthority.Text != "" || tbPassportNo.Text != "" || tbPhoneNumber.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void sprCustomerOne_Load(object sender, EventArgs e)
        {
            if (this.typeQuery == "edit")
            {
                loadElement();
            }
        }

         private void saveCustomer(string text) {
            try
            {
                Con.Open();
                string qText = text;
                OleDbCommand Com = new OleDbCommand();
                Com.Parameters.AddWithValue("@fio", tbFIO.Text);
                Com.Parameters.AddWithValue("@birth", Convert.ToDateTime(dtDateOfBirth.Text));
                Com.Parameters.AddWithValue("@passNum", tbPassportNo.Text);
                Com.Parameters.AddWithValue("@dateOfl", Convert.ToDateTime(dtDateOfIssue.Text));
                Com.Parameters.AddWithValue("@auth", tbAuthority.Text);
                Com.Parameters.AddWithValue("@adress", tbAddress.Text);
                Com.Parameters.AddWithValue("@phone", Convert.ToInt32(tbPhoneNumber.Text));
                Com.CommandText = qText;
                Com.Connection = Con;
                Com.ExecuteNonQuery();
                sprCustomerList sprCustomerList = new sprCustomerList();
                sprCustomerList.loadCustomers();
                this.Close();
            }
            catch (Exception err) {
                MessageBox.Show(err.Message);
            }
            finally {
                Con.Close();
            }            
        }

        private void loadElement() 
        {
            try
            {
                Con.Open();
                string qText = "SELECT * FROM Customers WHERE IDCUS = @id" ;
                OleDbCommand Com = new OleDbCommand();
                Com.Parameters.AddWithValue("@id", this.idQuery);
                Com.CommandText = qText;
                Com.Connection = Con;
                OleDbDataReader reader = Com.ExecuteReader();
                while (reader.Read())
                    {
                        tbFIO.Text = reader["FIO"].ToString();
                        dtDateOfBirth.Text = reader["DateOfBirth"].ToString();
                        tbPassportNo.Text = reader["PassportNo"].ToString();
                        dtDateOfIssue.Text = reader["DateOfIssue"].ToString();
                        tbAuthority.Text = reader["Authority"].ToString();
                        tbAddress.Text = reader["Address"].ToString();
                        tbPhoneNumber.Text = reader["PhoneNumber"].ToString();
                    }
            }
            catch (Exception err) {
                MessageBox.Show(err.Message);
            }
            finally {
                Con.Close();
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                if (this.typeQuery == "add") {
                    string text = "INSERT INTO Customers (FIO, DateOfBirth, PassportNo, DateOfIssue, Authority, Address, PhoneNumber) VALUES (@fio, @birth, @passNum, @dateOfl, @auth, @adress, @phone)";
                    saveCustomer(text);
                }
                else if (this.typeQuery == "edit"){
                    string text = "UPDATE Customers SET FIO = @fio, DateOfBirth = @birth, PassportNo = @passNum, DateOfIssue = @dateOfl, Authority = @auth, Address = @adress, PhoneNumber = @phone WHERE IDCUS = " + this.id;
                    saveCustomer(text);
                }               
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
