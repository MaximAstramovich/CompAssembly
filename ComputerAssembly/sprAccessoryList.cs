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
    public partial class sprAccessoryList : BaseForm
    {
        public sprAccessoryList()
        {
            InitializeComponent();
        }

        string Mode = "0";

        public string ModeCheck
        {
            set { Mode = value; }
        }

        private async void sprAccessoryList_Load(object sender, EventArgs e)
        {
            await loadComponents();
            tablProp();
        }

        DataTable currentDataTable = new DataTable();
        public async Task loadComponents() 
        {
            currentDataTable.Rows.Clear();
            currentDataTable.Columns.Clear();
            currentDataTable.Columns.Add("Номер", typeof(int));
            currentDataTable.Columns.Add("Тип", typeof(string));
            currentDataTable.Columns.Add("Название", typeof(string));
            currentDataTable.Columns.Add("Описание", typeof(string));
            currentDataTable.Columns.Add("Стоимость", typeof(decimal));
            try
            {
                var componentsList = await ComponentsBusinessLayer.GetAllComponentsListAsync();
                if (componentsList != null)
                {
                    foreach (var component in componentsList)
                    {
                        currentDataTable.Rows.Add(component.IDCOM, component.ComponentTypes.Type, component.Nazv, component.Description, component.Price);
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
                
                dgComponentsList.DataSource = currentDataTable;
                dgComponentsList.Columns["_RowString"].Visible = false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }     
        }

        public void tablProp()
        {
            this.dgComponentsList.AllowUserToAddRows = false;
            this.dgComponentsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgComponentsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgComponentsList.BackgroundColor = System.Drawing.Color.White;
            this.dgComponentsList.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text;

            dgComponentsList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dgComponentsList.Rows)
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
            sprAccessoryOne sprAccessoryOneForm = new sprAccessoryOne();
            sprAccessoryOneForm.type = "add";
            sprAccessoryOneForm.Text = "Новый элемент";
            sprAccessoryOneForm.LastComponentId = (int)dgComponentsList.Rows[dgComponentsList.RowCount - 1].Cells[0].Value;
            sprAccessoryOneForm.ShowDialog();
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
                    ComponentsBusinessLayer.Remove((int)dgComponentsList.CurrentRow.Cells[0].Value);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sprAccessoryOne sprAccessoryOneForm = new sprAccessoryOne();
            sprAccessoryOneForm.type = "edit";
            sprAccessoryOneForm.id = dgComponentsList.CurrentRow.Cells[0].Value.ToString();
            sprAccessoryOneForm.Text = dgComponentsList.CurrentRow.Cells[2].Value.ToString();
            sprAccessoryOneForm.ShowDialog();
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await loadComponents();
            tablProp();
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgComponentsList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Mode == "1")
            {
                sprReceiptOne main = this.Owner as sprReceiptOne;
                if (main != null)
                {
                    var component = ComponentsBusinessLayer.FindComponentById((int)dgComponentsList.CurrentRow.Cells[0].Value);
                    main.SetCurrentComponent = component;
                    main.UpdateComponentField();
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
