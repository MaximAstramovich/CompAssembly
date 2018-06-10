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
    public partial class sprCustomerList : BaseForm
    {
        //OleDbConnection Con = new OleDbConnection();
        public sprCustomerList()
        {
            InitializeComponent();
            //Con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=DB.mdb;Persist Security Info=False;";
        }

        string Mode = "0";

        public string ModeCheck {
            set { Mode = value; }
        }

        private async void sprCustomerList_Load(object sender, EventArgs e)
        {
            tablProp();
            await loadCustomers();
        }

        public async Task loadCustomers()
        {
            try
            {
                dgCustomerList.Rows.Clear();
                dgCustomerList.Columns.Clear();
                dgCustomerList.Columns.Add("id", "Номер");
                dgCustomerList.Columns.Add("fio", "ФИО");
                dgCustomerList.Columns.Add("address", "Адрес");
                dgCustomerList.Columns.Add("phone", "Телефон");
                var customersList = await CustomersBusinessLayer.GetAllCustomersListAsync();
                if (customersList.Count != 0)
                {
                    foreach (var customer in customersList)
                    {
                        dgCustomerList.Rows.Add(customer.IDCUS, customer.FIO, customer.Address, customer.PhoneNumber);
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public void tablProp()
        {

            this.dgCustomerList.AllowUserToAddRows = false;
            this.dgCustomerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCustomerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCustomerList.BackgroundColor = System.Drawing.Color.White;
            this.dgCustomerList.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dgCustomerList.Rows.Clear();
                dgCustomerList.Columns.Clear();
                dgCustomerList.Columns.Add("id", "Номер");
                dgCustomerList.Columns.Add("fio", "ФИО");
                dgCustomerList.Columns.Add("address", "Адрес");
                dgCustomerList.Columns.Add("phone", "Телефон");
                //Con.Open();
                //string qText = "SELECT * FROM Customers where IDCUS Like '%" + textBox1.Text + "%' OR FIO Like '%" + textBox1.Text + "%' OR Address Like '%" + textBox1.Text + "%' OR PhoneNumber Like '%" + textBox1.Text + "%';";
                //OleDbCommand Com = new OleDbCommand(qText, Con);
                //OleDbDataReader reader = Com.ExecuteReader();
                //while (reader.Read())
                //{
                //    dgCustomerList.Rows.Add(reader["IDCUS"], reader["FIO"], reader["Address"], reader["PhoneNumber"]);
                //}
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                //Con.Close();
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sprCustomerOne sprCustomerOneForm = new sprCustomerOne();
            sprCustomerOneForm.type = "add";
            sprCustomerOneForm.Text = "Новый клиент";
            sprCustomerOneForm.ShowDialog();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DialogResult dR = MessageBox.Show(
            //                 "Вы действительно желаете удалить запись?",
            //                 "Программа",
            //                 MessageBoxButtons.OKCancel,
            //                 MessageBoxIcon.Warning
            //             );
            //if (dR == DialogResult.OK)
            //{
            //    string componentsId = Convert.ToString(dgCustomerList.CurrentRow.Cells[0].Value);
            //    string qText = "DELETE FROM Customers WHERE IDCUS = @id";
            //    OleDbCommand Com = new OleDbCommand();
            //    Com.Parameters.AddWithValue("@id", componentsId);
            //    Com.CommandText = qText;
            //    Com.Connection = Con;
            //    try
            //    {
            //        Con.Open();
            //        Com.ExecuteNonQuery();
            //        Con.Close();
            //        loadCustomers();
            //    }
            //    catch (Exception err)
            //    {
            //        MessageBox.Show(err.Message);
            //    }
            //    finally
            //    {
            //        Con.Close();
            //    }
            //}
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sprCustomerOne sprCustomerOne = new sprCustomerOne();
            sprCustomerOne.type = "edit";
            sprCustomerOne.id = dgCustomerList.CurrentRow.Cells[0].Value.ToString();
            sprCustomerOne.Text = dgCustomerList.CurrentRow.Cells[1].Value.ToString();
            sprCustomerOne.ShowDialog();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadCustomers();
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgCustomerList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Mode == "1")
            {
                AssemblyOne main = this.Owner as AssemblyOne;
                if (main != null)
                {
                    main.Customer = dgCustomerList.CurrentRow.Cells[1].Value.ToString();
                    this.Close();
                }
            }
        }
    }
}
