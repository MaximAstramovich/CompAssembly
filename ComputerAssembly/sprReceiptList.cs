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
    public partial class sprReceiptList : BaseForm
    {
        public sprReceiptList()
        {
            InitializeComponent();
        }

        private async void sprReceiptList_Load(object sender, EventArgs e)
        {
            await loadReceipts();
            tablProp();
        }

        DataTable currentDataTable = new DataTable();
        public async Task loadReceipts()
        {
            try
            {
                currentDataTable.Rows.Clear();
                currentDataTable.Columns.Clear();
                currentDataTable.Columns.Add("idReceipt", typeof(int));
                currentDataTable.Columns.Add("Фирма-поставщик", typeof(string));
                currentDataTable.Columns.Add("Название компонента", typeof(string));
                currentDataTable.Columns.Add("Количество", typeof(int));
                currentDataTable.Columns.Add("Цена за штуку", typeof(decimal));
                currentDataTable.Columns.Add("Дата поставки", typeof(DateTime));
                var receiptsList = await ReceiptsBusinessLayer.GetAllReceiptsListAsync();
                if (receiptsList.Count != 0)
                {
                    foreach (var receipt in receiptsList)
                    {
                        currentDataTable.Rows.Add(receipt.IDR, receipt.Supplier.Firm, receipt.Component.Nazv, receipt.Quality, receipt.Price, receipt.ReceiptDate);
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

                dgReceiptList.DataSource = currentDataTable;
                dgReceiptList.Columns["_RowString"].Visible = false;
                dgReceiptList.Columns["idReceipt"].Visible = false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public void tablProp()
        {
            this.dgReceiptList.AllowUserToAddRows = false;
            this.dgReceiptList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReceiptList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgReceiptList.BackgroundColor = System.Drawing.Color.White;
            this.dgReceiptList.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text;

            dgReceiptList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dgReceiptList.Rows)
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
            sprReceiptOne sprReceiptOneForm = new sprReceiptOne();
            sprReceiptOneForm.type = "add";
            sprReceiptOneForm.Text = "Новая поставка";
            sprReceiptOneForm.ShowDialog();
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
                ReceiptsBusinessLayer.Remove((int)dgReceiptList.CurrentRow.Cells[0].Value);
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sprReceiptOne sprReceiptOne = new sprReceiptOne();
            sprReceiptOne.type = "edit";
            sprReceiptOne.IdReceipt = (int)dgReceiptList.CurrentRow.Cells[0].Value;
            sprReceiptOne.id = dgReceiptList.CurrentRow.Cells[0].Value.ToString();
            sprReceiptOne.Text = dgReceiptList.CurrentRow.Cells[1].Value.ToString();
            sprReceiptOne.ShowDialog();
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await loadReceipts();
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            currentDataTable.DefaultView.RowFilter = string.Format("[_RowString] LIKE '%{0}%'", textBox1.Text);
        }
    }
}
