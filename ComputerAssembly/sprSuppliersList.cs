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

        public async Task loadSuppliers()
        {
            try
            {
                dgSuppliersList.Rows.Clear();
                dgSuppliersList.Columns.Clear();
                dgSuppliersList.Columns.Add("id", "Номер");
                dgSuppliersList.Columns.Add("firm", "Фирма");
                dgSuppliersList.Columns.Add("address", "Юридический адрес");
                dgSuppliersList.Columns.Add("unn", "УНН");
                dgSuppliersList.Columns.Add("checkacc", "Расчетный счет");
                dgSuppliersList.Columns.Add("bank", "Код банка");
                dgSuppliersList.Columns.Add("fio", "ФИО представителя");
                dgSuppliersList.Columns.Add("pos", "Должность");
                dgSuppliersList.Columns.Add("phone", "Телефон");

                var suppliersList = await SuppliersBusinessLayer.GetAllSuppliersListAsync();
                foreach (var supplier in suppliersList)
                {
                    dgSuppliersList.Rows.Add(supplier.IdSuppliers, supplier.Firm, supplier.Address, supplier.UNN,
                        supplier.CheckingAccount, supplier.BankCode, supplier.FIO, supplier.Position, supplier.PhoneNumber);
                }
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
            try
            {
                dgSuppliersList.Rows.Clear();
                dgSuppliersList.Columns.Clear();
                dgSuppliersList.Columns.Add("id", "Номер");
                dgSuppliersList.Columns.Add("firm", "Фирма");
                dgSuppliersList.Columns.Add("address", "Юридический адрес");
                dgSuppliersList.Columns.Add("unn", "УНН");
                dgSuppliersList.Columns.Add("checkacc", "Расчетный счет");
                dgSuppliersList.Columns.Add("bank", "Код банка");
                dgSuppliersList.Columns.Add("fio", "ФИО представителя");
                dgSuppliersList.Columns.Add("pos", "Должность");
                dgSuppliersList.Columns.Add("phone", "Телефон");
                //Con.Open();
                //string qText = "SELECT * FROM Suppliers where IDSUP Like '%" + textBox1.Text + "%' OR Firm Like '%" + textBox1.Text + "%' OR Address Like '%" + textBox1.Text + "%' OR UNN Like '%" + textBox1.Text + "%' OR CheckingAccount Like '%" + textBox1.Text + "%' OR BankCode Like '%" + textBox1.Text + "%' OR FIO Like '%" + textBox1.Text + "%' OR Position Like '%" + textBox1.Text + "%' OR PhoneNumber Like '%" + textBox1.Text + "%';";
                //OleDbCommand Com = new OleDbCommand(qText, Con);
                //OleDbDataReader reader = Com.ExecuteReader();
                //while (reader.Read())
                //{
                //    dgSuppliersList.Rows.Add(reader["IDSUP"], reader["Firm"], reader["Address"], reader["UNN"], reader["CheckingAccount"], reader["BankCode"], reader["FIO"], reader["Position"], reader["PhoneNumber"]);
                //}
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
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
    }
}
