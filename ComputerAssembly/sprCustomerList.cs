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
using DAL.DBModel;
using DAL.Models;

namespace ComputerAssembly
{
    public partial class sprCustomerList : BaseForm
    {
        public sprCustomerList()
        {
            InitializeComponent();
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

        DataTable currentDataTable = new DataTable();
        public async Task loadCustomers()
        {
            try
            {
                currentDataTable.Rows.Clear();
                currentDataTable.Columns.Clear();
                currentDataTable.Columns.Add("Номер", typeof(int));
                currentDataTable.Columns.Add("ФИО", typeof(string));
                currentDataTable.Columns.Add("Адрес", typeof(string));
                currentDataTable.Columns.Add("Телефон", typeof(int));
                var customersList = await CustomersBusinessLayer.GetAllCustomersListAsync();
                if (customersList.Count != 0)
                {
                    foreach (var customer in customersList)
                    {
                        currentDataTable.Rows.Add(customer.IDCUS, customer.FIO, customer.Address, customer.PhoneNumber);
                    }
                }
                DataColumn dcRowString = currentDataTable.Columns.Add("_RowString", typeof(string));
                foreach (DataRow dataRow in currentDataTable.Rows)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < currentDataTable.Columns.Count - 1; i++)
                    {
                        sb.Append(dataRow[i].ToString());
                        sb.Append("\t");
                    }
                    dataRow[dcRowString] = sb.ToString();
                }

                dgCustomerList.DataSource = currentDataTable;
                dgCustomerList.Columns["_RowString"].Visible = false;
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
            string searchValue = textBox1.Text;

            dgCustomerList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dgCustomerList.Rows)
                {
                    if (row.Cells[2].Value.ToString().Contains(searchValue))
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
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
            DialogResult dR = MessageBox.Show(
                             "Вы действительно желаете удалить запись?",
                             "Программа",
                             MessageBoxButtons.OKCancel,
                             MessageBoxIcon.Warning
                         );
            if (dR == DialogResult.OK)
            {
                CustomersBusinessLayer.Remove((int)dgCustomerList.CurrentRow.Cells[0].Value);
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sprCustomerOne sprCustomerOne = new sprCustomerOne();
            sprCustomerOne.type = "edit";
            sprCustomerOne.id = dgCustomerList.CurrentRow.Cells[0].Value.ToString();
            sprCustomerOne.Text = dgCustomerList.CurrentRow.Cells[1].Value.ToString();
            sprCustomerOne.ShowDialog();
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await loadCustomers();
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
                    main.IdCustomer = (int)dgCustomerList.CurrentRow.Cells[0].Value;
                    main.Customer = dgCustomerList.CurrentRow.Cells[1].Value.ToString();
                    this.Close();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            currentDataTable.DefaultView.RowFilter = string.Format("[_RowString] LIKE '%{0}%'", textBox1.Text);
        }
    }
}
