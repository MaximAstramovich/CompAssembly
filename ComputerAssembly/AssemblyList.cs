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
    public partial class AssemblyList : BaseForm
    {
        public AssemblyList()
        {
            InitializeComponent();
            _assemblyNumbers = new List<int?>();
            _assemblyList = new List<AssemblyModel>();
        }

        List<AssemblyModel> _assemblyList;
        List<int?> _assemblyNumbers { get; set; } 

        private async void AssemblyList_Load(object sender, EventArgs e)
        {
            await loadAssembly();
            tablProp();
        }

        DataTable currentDataTable = new DataTable();
        private async Task loadAssembly()
        {
            try
            {
                currentDataTable.Rows.Clear();
                currentDataTable.Columns.Clear();
                currentDataTable.Columns.Add("id", typeof(int));
                currentDataTable.Columns.Add("Клиент", typeof(string));
                currentDataTable.Columns.Add("Номер сборки", typeof(int));
                currentDataTable.Columns.Add("Дата сборки", typeof(DateTime));
                currentDataTable.Columns.Add("Сумма", typeof(decimal));
                currentDataTable.Columns.Add("Статус", typeof(string));
                currentDataTable.Columns.Add("Дата выдачи", typeof(DateTime));
                _assemblyList = await AssemblyBusinessLayer.GetAllAssemblyListAsync();
                _assemblyNumbers.AddRange(_assemblyList.Select(x => x.Num));
                if (_assemblyList.Count != 0)
                {
                    foreach (var assembly in _assemblyList)
                    {
                        string currentStatus = assembly.Status != null ? assembly.Status == 0 ? "Не выдано" : "Выдано" : "Нет данных";
                        currentDataTable.Rows.Add(assembly.IdAssembly, assembly.Customers.FIO, assembly.Num, assembly.OrderDate, assembly.Summ, currentStatus, assembly.DateOfPayment);
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

                dgAssembly.DataSource = currentDataTable;
                dgAssembly.Columns["_RowString"].Visible = false;
                dgAssembly.Columns["id"].Visible = false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public void tablProp()
        {

            this.dgAssembly.AllowUserToAddRows = false;
            this.dgAssembly.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAssembly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAssembly.BackgroundColor = System.Drawing.Color.White;
            this.dgAssembly.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text;

            dgAssembly.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dgAssembly.Rows)
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
            AssemblyOne AssemblyOneForm = new AssemblyOne();
            AssemblyOneForm.assemblyNumbers = _assemblyNumbers;
            AssemblyOneForm.Show();
        }

        private async void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dR = MessageBox.Show(
                             "Вы действительно желаете удалить запись?",
                             "Программа",
                             MessageBoxButtons.OKCancel,
                             MessageBoxIcon.Warning
                         );
            if (dR == DialogResult.OK)
            {
                AssemblyBusinessLayer.Remove((int)dgAssembly.CurrentRow.Cells[0].Value);
                await loadAssembly();
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssemblyOne AssemblyOneForm = new AssemblyOne();
            AssemblyOneForm.IdAssembly = (int)dgAssembly.CurrentRow.Cells[0].Value;
            AssemblyOneForm.assemblyNumbers = _assemblyNumbers;
            AssemblyOneForm.ID = dgAssembly.CurrentRow.Cells[1].Value.ToString();
            AssemblyOneForm.Type = "edit";
            AssemblyOneForm.Show();
        }

        private void выдатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string status = dgAssembly.CurrentRow.Cells[5].Value.ToString();
            if (status == "Выдано")
            {
                MessageBox.Show("Сборка уже выдана владельцу");
            }
            else
            {
                DialogResult dR = MessageBox.Show(
                             "Выдать сборку?",
                             "Программа",
                             MessageBoxButtons.OKCancel,
                             MessageBoxIcon.Question
                         );
                if (dR == DialogResult.OK)
                {
                    giveOut();
                }
            }
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await loadAssembly();
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void giveOut()
        {
            var stockList = AssemblyBusinessLayer.GetStockList();
            var selectedAssembly = _assemblyList.FirstOrDefault(x => x.IdAssembly == (int)dgAssembly.CurrentRow.Cells[0].Value);
            selectedAssembly.DateOfPayment = DateTime.Today;
            selectedAssembly.Status = 1;
            AssemblyBusinessLayer.AddOrUpdateAssembly(selectedAssembly.IdAssembly, (int)selectedAssembly.Audio, (int)selectedAssembly.Board,
                (int)selectedAssembly.Corpus, (int)selectedAssembly.CPU, selectedAssembly.DateOfPayment, (int)selectedAssembly.DVD,
                (int)selectedAssembly.Graphic, (int)selectedAssembly.HDD, (int)selectedAssembly.Ice, (int)selectedAssembly.IDCUS,
                (int)selectedAssembly.Num, (DateTime)selectedAssembly.OrderDate, (int)selectedAssembly.OZU, (int)selectedAssembly.Power,
                (int)selectedAssembly.SSD, (int)selectedAssembly.Status, (decimal)selectedAssembly.Summ);
            AssemblyBusinessLayer.AddSell(0, 0, selectedAssembly.IDCUS, selectedAssembly.Summ, 1, selectedAssembly.DateOfPayment.ToString());
            foreach (var item in stockList)
            {
                if (item.IdStock == selectedAssembly.Audio)
                {
                    int count = item.InStock >= 1 ? (int)item.InStock - 1 : 0;
                    AssemblyBusinessLayer.UpdateStock(item.IdStock, count);
                }
                if (item.IdStock == selectedAssembly.Board)
                {
                    int count = item.InStock >= 1 ? (int)item.InStock - 1 : 0;
                    AssemblyBusinessLayer.UpdateStock(item.IdStock, count);
                }
                if (item.IdStock == selectedAssembly.Corpus)
                {
                    int count = item.InStock >= 1 ? (int)item.InStock - 1 : 0;
                    AssemblyBusinessLayer.UpdateStock(item.IdStock, count);
                }
                if (item.IdStock == selectedAssembly.CPU)
                {
                    int count = item.InStock >= 1 ? (int)item.InStock - 1 : 0;
                    AssemblyBusinessLayer.UpdateStock(item.IdStock, count);
                }
                if (item.IdStock == selectedAssembly.DVD)
                {
                    int count = item.InStock >= 1 ? (int)item.InStock - 1 : 0;
                    AssemblyBusinessLayer.UpdateStock(item.IdStock, count);
                }
                if (item.IdStock == selectedAssembly.Graphic)
                {
                    int count = item.InStock >= 1 ? (int)item.InStock - 1 : 0;
                    AssemblyBusinessLayer.UpdateStock(item.IdStock, count);
                }
                if (item.IdStock == selectedAssembly.HDD)
                {
                    int count = item.InStock >= 1 ? (int)item.InStock - 1 : 0;
                    AssemblyBusinessLayer.UpdateStock(item.IdStock, count);
                }
                if (item.IdStock == selectedAssembly.Ice)
                {
                    int count = item.InStock >= 1 ? (int)item.InStock - 1 : 0;
                    AssemblyBusinessLayer.UpdateStock(item.IdStock, count);
                }
                if (item.IdStock == selectedAssembly.OZU)
                {
                    int count = item.InStock >= 1 ? (int)item.InStock - 1 : 0;
                    AssemblyBusinessLayer.UpdateStock(item.IdStock, count);
                }
                if (item.IdStock == selectedAssembly.Power)
                {
                    int count = item.InStock >= 1 ? (int)item.InStock - 1 : 0;
                    AssemblyBusinessLayer.UpdateStock(item.IdStock, count);
                }
                if (item.IdStock == selectedAssembly.SSD)
                {
                    int count = item.InStock >= 1 ? (int)item.InStock - 1 : 0;
                    AssemblyBusinessLayer.UpdateStock(item.IdStock, count);
                }
            }
            await loadAssembly();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            currentDataTable.DefaultView.RowFilter = string.Format("[_RowString] LIKE '%{0}%'", textBox1.Text);
        }
    }
}

