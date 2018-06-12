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

        public async Task loadReceipts()
        {
            try
            {
                dgReceiptList.Rows.Clear();
                dgReceiptList.Columns.Clear();
                dgReceiptList.Columns.Add("idReceipt", "Id");
                dgReceiptList.Columns.Add("firm", "Фирма-поставщик");
                dgReceiptList.Columns.Add("name", "Название компонента");
                dgReceiptList.Columns.Add("quality", "Количество");
                dgReceiptList.Columns.Add("price", "Цена за штуку");
                dgReceiptList.Columns.Add("rdate", "Дата поставки");
                dgReceiptList.Columns.GetFirstColumn(DataGridViewElementStates.Visible).Visible = false;
                var receiptsList = await ReceiptsBusinessLayer.GetAllReceiptsListAsync();
                if (receiptsList.Count != 0)
                {
                    foreach (var receipt in receiptsList)
                    {
                        dgReceiptList.Rows.Add(receipt.IDR, receipt.Supplier.Firm, receipt.Component.Nazv, receipt.Quality, receipt.Price, receipt.ReceiptDate);
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
            this.dgReceiptList.AllowUserToAddRows = false;
            this.dgReceiptList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReceiptList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgReceiptList.BackgroundColor = System.Drawing.Color.White;
            this.dgReceiptList.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //dgReceiptList.Rows.Clear();
                //dgReceiptList.Columns.Clear();
                //dgReceiptList.Columns.Add("idsup", "ID поставщика");
                //dgReceiptList.Columns.Add("idcom", "ID компонента");
                //dgReceiptList.Columns.Add("quality", "Количество");
                //dgReceiptList.Columns.Add("rdate", "Дата поставки");
                //Con.Open();
                //string qText = "SELECT * FROM Receipts where IDSUP Like '%" + textBox1.Text + "%' OR IDCOM Like '%" + textBox1.Text + "%' OR Qulity Like '%" + textBox1.Text + "%' OR ReceiptDate Like '%" + textBox1.Text + "%';";
                //OleDbCommand Com = new OleDbCommand(qText, Con);
                //OleDbDataReader reader = Com.ExecuteReader();
                //while (reader.Read())
                //{
                //    dgReceiptList.Rows.Add(reader["IDSUP"], reader["IDCOM"], reader["Quality"], reader["ReceiptDate"], reader["PhoneNumber"]);
                //}
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
    }
}
