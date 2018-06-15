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
    public partial class sprSuppliersList : BaseForm
    {
        public sprSuppliersList()
        {
            InitializeComponent();
        }

        string Mode = "0";

        public string ModeCheck
        {
            set { Mode = value; }
        }


        private async void sprSuppliersList_Load(object sender, EventArgs e)
        {
            await loadSuppliers();
            tablProp();
        }

        DataTable currentDataTable = new DataTable();
        public async Task loadSuppliers()
        {
            try
            {
                currentDataTable.Rows.Clear();
                currentDataTable.Columns.Clear();
                currentDataTable.Columns.Add("Номер", typeof(int));
                currentDataTable.Columns.Add("Фирма", typeof(string));
                currentDataTable.Columns.Add("Юридический адрес", typeof(string));
                currentDataTable.Columns.Add("УНН", typeof(int));
                currentDataTable.Columns.Add("Расчетный счет", typeof(int));
                currentDataTable.Columns.Add("Код банка", typeof(string));
                currentDataTable.Columns.Add("ФИО представителя", typeof(string));
                currentDataTable.Columns.Add("Должность", typeof(string));
                currentDataTable.Columns.Add("Телефон", typeof(int));
                var suppliersList = await SuppliersBusinessLayer.GetAllSuppliersListAsync();
                if (suppliersList != null)
                {
                    foreach (var supplier in suppliersList)
                    {
                        currentDataTable.Rows.Add(supplier.IdSuppliers, supplier.Firm, supplier.Address, supplier.UNN,
                            supplier.CheckingAccount, supplier.BankCode, supplier.FIO, supplier.Position, supplier.PhoneNumber);
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

                dgSuppliersList.DataSource = currentDataTable;
                dgSuppliersList.Columns["_RowString"].Visible = false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public void tablProp()
        {
            this.dgSuppliersList.AllowUserToAddRows = false;
            this.dgSuppliersList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSuppliersList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSuppliersList.BackgroundColor = System.Drawing.Color.White;
            this.dgSuppliersList.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text;

            dgSuppliersList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dgSuppliersList.Rows)
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
            sprSuppliersOne sprSuppliersOneForm = new sprSuppliersOne();
            sprSuppliersOneForm.type = "add";
            sprSuppliersOneForm.Text = "Новый поставщик";
            sprSuppliersOneForm.ShowDialog();
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
                try
                {
                    SuppliersBusinessLayer.Remove((int)dgSuppliersList.CurrentRow.Cells[0].Value);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sprSuppliersOne sprSuppliersOne = new sprSuppliersOne();
            sprSuppliersOne.type = "edit";
            sprSuppliersOne.id = dgSuppliersList.CurrentRow.Cells[0].Value.ToString();
            sprSuppliersOne.Text = dgSuppliersList.CurrentRow.Cells[1].Value.ToString();
            sprSuppliersOne.ShowDialog();
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await loadSuppliers();
            tablProp();
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgSuppliersList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Mode == "1")
            {
                sprReceiptOne main = this.Owner as sprReceiptOne;
                if (main != null)
                {
                    var supplier = SuppliersBusinessLayer.FindSupplierById((int)dgSuppliersList.CurrentRow.Cells[0].Value);
                    main.SetCurrentSupplier = supplier;
                    main.UpdateSupplierFioField();
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
