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
    public partial class AssemblyList : Form
    {
        OleDbConnection Con = new OleDbConnection();
        public AssemblyList()
        {
            InitializeComponent();
            Con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=DB.mdb;Persist Security Info=False;";
        }

        private void AssemblyList_Load(object sender, EventArgs e)
        {
            loadAssembly();
            tablProp();
        }

        private void loadAssembly()
        {
            try
            {
                Con.Open();
                dgAssembly.Rows.Clear();
                dgAssembly.Columns.Clear();
                dgAssembly.Columns.Add("customers", "Клиент");
                dgAssembly.Columns.Add("number", "Номер сборки");
                dgAssembly.Columns.Add("OrderDate", "Дата сборки");
                dgAssembly.Columns.Add("Summ", "Сумма");
                dgAssembly.Columns.Add("status", "Статус");
                dgAssembly.Columns.Add("DateOfPayment", "Дата выдачи");
                string qText = "SELECT a.OrderDate, a.Num, a.Summ, a.Status,a.DateOfPayment, c.FIO FROM Assembly a LEFT JOIN Customers c ON a.IDCUS = c.IDCUS ORDER BY a.OrderDate ";
                OleDbCommand Com = new OleDbCommand(qText, Con);
                OleDbDataReader reader = Com.ExecuteReader();
                while (reader.Read())
                {
                    dgAssembly.Rows.Add(reader["FIO"], reader["Num"], reader["OrderDate"], reader["Summ"], (int)reader["Status"] == 0 ? "Не выдано" : "Выдано", reader["DateOfPayment"]);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                Con.Close();
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
            for (int i = 0; i < dgAssembly.RowCount; i++)
            {
                dgAssembly.Rows[i].Selected = false;
                for (int j = 0; j < dgAssembly.ColumnCount; j++)
                    if (dgAssembly.Rows[i].Cells[j].Value != null)
                        if (dgAssembly.Rows[i].Cells[j].Value.ToString().ToLower().Contains(textBox1.Text.ToLower()))
                        {
                            dgAssembly.Rows[i].Selected = true;
                            break;
                        }
                if (textBox1.Text == "") dgAssembly.Rows[i].Selected = false;
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssemblyOne AssemblyOneForm = new AssemblyOne();
            AssemblyOneForm.Show();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string num = dgAssembly.CurrentRow.Cells[1].Value.ToString();
            deleteElement(num);
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssemblyOne AssemblyOneForm = new AssemblyOne();
            AssemblyOneForm.ID = dgAssembly.CurrentRow.Cells[1].Value.ToString();
            AssemblyOneForm.Type = "edit";
            AssemblyOneForm.Show();
        }

        private void выдатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string status = dgAssembly.CurrentRow.Cells[4].Value.ToString();
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

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadAssembly();
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteElement(string num)
        {
            string qText = "DELETE FROM Assembly a WHERE a.Num = @num";
            OleDbCommand Com = new OleDbCommand(qText, Con);
            Com.Parameters.AddWithValue("@num", num);
            DialogResult dR = MessageBox.Show(
                             "Вы действительно желаете удалить запись?",
                             "Программа",
                             MessageBoxButtons.OKCancel,
                             MessageBoxIcon.Warning
                         );
            if (dR == DialogResult.OK)
            {
                this.Con.Open();
                Com.ExecuteNonQuery();
                this.Con.Close();
                loadAssembly();
            }
        }

        private void giveOut()
        {
            string num = dgAssembly.CurrentRow.Cells[1].Value.ToString();
            string qTextUpdate = "UPDATE Assembly a SET a.Status = 1, a.DateOfPayment = @dat";
            OleDbCommand ComUpdate = new OleDbCommand(qTextUpdate, Con);
            ComUpdate.Parameters.AddWithValue("@dat", DateTime.Today);
            try
            {
                this.Con.Open();
                ComUpdate.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                this.Con.Close();
            }
            loadAssembly();
        }
    }
}

