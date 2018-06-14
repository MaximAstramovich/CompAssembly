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

        public async Task loadComponents() {
            dgComponentsList.Rows.Clear();
            dgComponentsList.Columns.Clear();
            dgComponentsList.Columns.Add("id", "Номер");
            dgComponentsList.Columns.Add("type", "Тип");
            dgComponentsList.Columns.Add("name", "Название");
            dgComponentsList.Columns.Add("description", "Описание");
            dgComponentsList.Columns.Add("price", "Стоимость");
            try
            {
                var componentsList = await ComponentsBusinessLayer.GetAllComponentsListAsync();
                if (componentsList != null)
                {
                    foreach (var component in componentsList)
                    {
                        dgComponentsList.Rows.Add(component.IDCOM, component.ComponentTypes.Type, component.Nazv, component.Description, component.Price);
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
            this.dgComponentsList.AllowUserToAddRows = false;
            this.dgComponentsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgComponentsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgComponentsList.BackgroundColor = System.Drawing.Color.White;
            this.dgComponentsList.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgComponentsList.RowCount; i++) 
            {
                dgComponentsList.Rows[i].Selected = false;
                for (int j = 0; j < dgComponentsList.ColumnCount; j++)
                    if (dgComponentsList.Rows[i].Cells[j].Value != null)
                        if (dgComponentsList.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text)) 
                        {
                            dgComponentsList.Rows[i].Selected = true; 
                            break; 
                        } 
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sprAccessoryOne sprAccessoryOneForm = new sprAccessoryOne();
            sprAccessoryOneForm.type = "add";
            sprAccessoryOneForm.Text = "Новый элемент";
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
    }
}
