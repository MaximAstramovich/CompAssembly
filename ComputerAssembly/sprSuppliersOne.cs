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
    public partial class sprSuppliersOne : Form
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
            public sprSuppliersOne()
            {
                InitializeComponent();
                Con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=DB.mdb;Persist Security Info=False;";
            }

            private bool validate()
            {

                if (tbFIO.Text != "" || tbFirm.Text != "" || tbPosition.Text != "" || tbPhoneNumber.Text != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            private void sprSuppliersOne_Load(object sender, EventArgs e)
            {
                if (this.typeQuery == "edit")
                {
                    loadElement();
                }
            }

            private void saveSuppliers(string text)
            {
                try
                {
                    Con.Open();
                    string qText = text;
                    OleDbCommand Com = new OleDbCommand();
                    Com.Parameters.AddWithValue("@fio", tbFIO.Text);
                    Com.Parameters.AddWithValue("@firm", tbFirm.Text);
                    Com.Parameters.AddWithValue("@pos", tbPosition.Text);
                    Com.Parameters.AddWithValue("@phone", Convert.ToInt32(tbPhoneNumber.Text));
                    Com.CommandText = qText;
                    Com.Connection = Con;
                    Com.ExecuteNonQuery();
                    sprSuppliersList sprSuppliersList = new sprSuppliersList();
                    sprSuppliersList.loadSuppliers();
                    this.Close();
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

            private void loadElement()
            {
                try
                {
                    Con.Open();
                    string qText = "SELECT * FROM Suppliers WHERE IDSUP = @id";
                    OleDbCommand Com = new OleDbCommand();
                    Com.Parameters.AddWithValue("@id", this.idQuery);
                    Com.CommandText = qText;
                    Com.Connection = Con;
                    OleDbDataReader reader = Com.ExecuteReader();
                    while (reader.Read())
                    {
                        tbFIO.Text = reader["FIO"].ToString();
                        tbFirm.Text = reader["Firm"].ToString();
                        tbPosition.Text = reader["Position"].ToString();
                        tbPhoneNumber.Text = reader["PhoneNumber"].ToString();
                    }
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

            private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (validate())
                {
                    if (this.typeQuery == "add")
                    {
                        string text = "INSERT INTO Suppliers (FIO, Firm, Position, PhoneNumber) VALUES (@fio, @firm, @pos, @phone)";
                        saveSuppliers(text);
                    }
                    else if (this.typeQuery == "edit")
                    {
                        string text = "UPDATE Suppliers SET FIO = @fio, Firm = @firm, Position = @pos, PhoneNumber = @phone WHERE IDSUP = " + this.id;
                        saveSuppliers(text);
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
