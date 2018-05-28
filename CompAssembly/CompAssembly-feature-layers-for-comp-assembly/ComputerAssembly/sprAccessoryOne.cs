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
    public partial class sprAccessoryOne : Form
    {
        OleDbConnection Con = new OleDbConnection();

        public sprAccessoryOne()
        {
            InitializeComponent();
            Con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=DB.mdb;Persist Security Info=False;";
        }

        string typeQuery;
        string idQuery;
        string Mode;

        public string type {
            set {
                typeQuery = value;
            }
        }

        public string tBoxName
        {
            get { return tbName.Text; }
            set { tbName.Text = value; }
        }

        public string tBoxPrice
        {
            get { return tbPrice.Text; }
            set { tbPrice.Text = value; }
        }

        public string tBoxType
        {
            get { return cbType.Text; }
            set { cbType.Text = value; }
        }

        public string tBoxDescription
        {
            get { return rtbDescription.Text; }
            set { rtbDescription.Text = value; }
        }

        public string id
        {
            set { idQuery = value; }
            get { return idQuery; }
        }

        public string readMode
        {
            set { Mode = value; }
            get { return Mode; }
        }

        private void sprAccessoryOne_Load(object sender, EventArgs e)
        {
            if (this.Mode == "1") {
                tbName.ReadOnly = true;
                tbPrice.ReadOnly = true;
                rtbDescription.ReadOnly = true;
                сохранитьToolStripMenuItem.Visible = false;
            }

            string qText = "SELECT ct.Type FROM ComponentTypes ct ORDER BY ct.Type";
            try {
                Con.Open();
                OleDbCommand Com = new OleDbCommand(qText, Con);
                OleDbDataReader reader = Com.ExecuteReader();               
                while (reader.Read()) {
                    cbType.Items.Add(reader["Type"]);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally {
                Con.Close();
            }
        }

        private int getTypeId(string id)
        {
            string qText = "SELECT ct.ID FROM ComponentTypes ct WHERE ct.Type = @id";
            OleDbCommand Com = new OleDbCommand();
            Com.Parameters.AddWithValue("@id", id);
            Com.CommandText = qText;
            Com.Connection = Con;
            int elementId = (Int32)Com.ExecuteScalar();
            return elementId;
        }

        private void saveComponents(string text)
        {
            Con.Open();
            string qText = text;
            OleDbCommand Com = new OleDbCommand();
            Com.Parameters.AddWithValue("type", getTypeId(cbType.Text));
            Com.Parameters.AddWithValue("naim", tbName.Text);
            Com.Parameters.AddWithValue("description", rtbDescription.Text);
            Com.Parameters.AddWithValue("price", tbPrice.Text);
            Com.CommandText = qText;
            Com.Connection = Con;
            Com.ExecuteNonQuery();
            Con.Close();
            this.Close();
            sprAccessoryList sprAccessoryList = new sprAccessoryList();
            sprAccessoryList.loadComponents();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
                if (tbName.Text == "" || cbType.Text == "" || tbPrice.Text == "")
                {
                    MessageBox.Show("Заполните необходимые поля");
                }
                else {
                    if (typeQuery == "add") {
                        string text = "INSERT INTO Components (Type, Nazv, Description, Price) VALUES (@type, @naim, @description, @price)";
                        saveComponents(text);
                    }
                    else if (typeQuery == "edit") { 
                        string text = "UPDATE Components SET Type = @type, Nazv = @naim, Description = @description, Price = @price WHERE IDCOM = " + this.id;
                        saveComponents(text);
                    }
                }
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
