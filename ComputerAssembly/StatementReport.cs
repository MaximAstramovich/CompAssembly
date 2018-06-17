using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerAssembly
{
    public partial class StatementReport : BaseForm
    {
        public StatementReport()
        {
            InitializeComponent();
        }

        DataTable currentDataTable = new DataTable();
        private async Task LoadData()
        {
            try
            {
                currentDataTable.Rows.Clear();
                currentDataTable.Columns.Clear();
                currentDataTable.Columns.Add("№", typeof(int));
                currentDataTable.Columns.Add("Название компонента", typeof(string));
                currentDataTable.Columns.Add("Поступило", typeof(int));
                currentDataTable.Columns.Add("Сумма поступления", typeof(decimal));
                currentDataTable.Columns.Add("Продано", typeof(int));
                currentDataTable.Columns.Add("Сумма проданного", typeof(decimal));
                currentDataTable.Columns.Add("Остаток на складе", typeof(int));
                currentDataTable.Columns.Add("Сумма остатка", typeof(decimal));
                int counter = 0;
                var receiptsList = await ReceiptsBusinessLayer.GetAllReceiptsListAsync();
                var assemblyList = await AssemblyBusinessLayer.GetAllAssemblyListAsync();
                if (receiptsList.Count != 0)
                {
                    foreach (var receipt in receiptsList)
                    {
                        var currentSellList = assemblyList.FindAll(x => x.Audio == receipt.IDCOM || x.Board == receipt.IDCOM ||
                            x.Corpus == receipt.IDCOM || x.CPU == receipt.IDCOM || x.DVD == receipt.IDCOM || x.Graphic == receipt.IDCOM ||
                            x.HDD == receipt.IDCOM || x.Ice == receipt.IDCOM || x.OZU == receipt.IDCOM || x.Power == receipt.IDCOM ||
                            x.SSD == receipt.IDCOM).Where(z => z.Status == 1);
                        decimal summ = (decimal)receipt.Price * (decimal)receipt.Quality;
                        decimal stockSumm = (decimal)receipt.Component.Stock.InStock * (decimal)receipt.Price;
                        decimal sellSumm = (decimal)currentSellList.Count() * (decimal)receipt.Price;
                        currentDataTable.Rows.Add(++counter, receipt.Component.Nazv, receipt.Quality, summ, currentSellList.Count(), sellSumm, receipt.Component.Stock.InStock, stockSumm);
                        //foreach (var assembly in assemblyList)
                        //{
                        //    if ((receipt.IDCOM == assembly.Audio ||
                        //        receipt.IDCOM == assembly.Board ||
                        //        receipt.IDCOM == assembly.Corpus ||
                        //        receipt.IDCOM == assembly.CPU ||
                        //        receipt.IDCOM == assembly.DVD ||
                        //        receipt.IDCOM == assembly.Graphic ||
                        //        receipt.IDCOM == assembly.HDD ||
                        //        receipt.IDCOM == assembly.Ice ||
                        //        receipt.IDCOM == assembly.OZU ||
                        //        receipt.IDCOM == assembly.Power ||
                        //        receipt.IDCOM == assembly.SSD) && assembly.Status == 1)
                        //    {
 
                        //    }
                                
                        //}
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

                dgStatementList.DataSource = currentDataTable;
                dgStatementList.Columns["_RowString"].Visible = false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public void tablProp()
        {
            this.dgStatementList.AllowUserToAddRows = false;
            this.dgStatementList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgStatementList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgStatementList.BackgroundColor = System.Drawing.Color.White;
            this.dgStatementList.ReadOnly = true;
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void StatementReport_Load(object sender, EventArgs e)
        {
            tablProp();
            await LoadData();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            currentDataTable.DefaultView.RowFilter = string.Format("[_RowString] LIKE '%{0}%'", tbSearch.Text);
        }
    }
}
