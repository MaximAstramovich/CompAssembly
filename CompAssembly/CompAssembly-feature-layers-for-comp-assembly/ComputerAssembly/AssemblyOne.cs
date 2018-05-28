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
    public partial class AssemblyOne : Form
    {
        OleDbConnection Con = new OleDbConnection();
        public AssemblyOne()
        {
            InitializeComponent();
            Con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=DB.mdb;Persist Security Info=False;";
        }

        string typeQuery;
        string idQuery;

        public string Customer 
        {
            set { tbCustomer.Text = value; }
            get { return tbCustomer.Text; }
        }
        Dictionary <string, string> arr = new Dictionary<string,string>();

        public string Type {
            set { typeQuery = value; }
        }

        public string ID
        {
            set { idQuery = value; }
            get { return idQuery; }
        }

        private void AssemblyOne_Load(object sender, EventArgs e)
        {
            tbNumber.Text = getNumber().ToString();
            loadComponents();
            if (this.typeQuery == "edit")
            {
                loadAssembly();
                itogSumm();
            }
        }

        public void saveElement(string text) 
        {
            string qText = text;
            OleDbCommand Com = new OleDbCommand();
        }

        private int getNumber() { 
            Con.Open();
            Int32 number = 0;           
            try {
                string qText = "SELECT MAX(a.Num) FROM Assembly a";
                OleDbCommand Com = new OleDbCommand(qText, Con);
                number = Convert.ToInt32(Com.ExecuteScalar());  
            }
            catch (Exception err) {
                MessageBox.Show(err.Message);
            }
            finally {
                Con.Close();
            }
            return number + 1;
        }

        private void loadAssembly()
        {
            string qText = "SELECT a.* FROM Assembly a WHERE a.Num = @num";
            try
            {
                OleDbCommand Com = new OleDbCommand(qText, Con);
                Com.Parameters.AddWithValue("@num", this.idQuery);
                Con.Open();
                OleDbDataReader reader = Com.ExecuteReader();
                string Corpus = "";
                string Board = "";
                string CPU = "";
                string Graphic = "";
                string OZU = "";
                string HDD = "";
                string SSD = "";
                string Power = "";
                string DVD = "";
                string Audio = "";
                string Ice = "";
                string CustomerC = "";
                while (reader.Read())
                {
                    Corpus = getComponentById(reader["Corpus"].ToString());
                    Board = getComponentById(reader["Board"].ToString());
                    CPU = getComponentById(reader["CPU"].ToString());
                    Graphic = getComponentById(reader["Graphic"].ToString());
                    OZU = getComponentById(reader["OZU"].ToString());
                    HDD = getComponentById(reader["HDD"].ToString());
                    SSD = getComponentById(reader["SSD"].ToString());
                    Power = getComponentById(reader["Power"].ToString());
                    DVD = getComponentById(reader["DVD"].ToString());
                    Audio = getComponentById(reader["Audio"].ToString());
                    Ice = getComponentById(reader["Ice"].ToString());
                    CustomerC = getCustomerById(reader["IDCUS"].ToString());      
                }
                Con.Close();
                cbCorpus.Text = Corpus;
                cbBoard.Text = Board;
                cbCPU.Text = CPU;
                cbGraphic.Text = Graphic;
                cbOZU.Text = OZU;
                cbHDD.Text = HDD;
                cbSSD.Text = SSD;
                cbPower.Text = Power;
                cbDVD.Text = DVD;
                cbAudio.Text = Audio;
                cbIce.Text = Ice;
                tbCustomer.Text = CustomerC;
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

        private void loadComponents() {
            arr.Add("1", "cbCorpus");
            arr.Add("2", "cbBoard");
            arr.Add("3", "cbCPU");
            arr.Add("4", "cbGraphic");
            arr.Add("5", "cbOZU");
            arr.Add("6", "cbHDD");
            arr.Add("7", "cbSSD");
            arr.Add("8", "cbPower");
            arr.Add("9", "cbDVD");
            arr.Add("10", "cbAudio");
            arr.Add("11", "cbIce");           
            Con.Open();
            foreach(KeyValuePair<string, string> type in arr){               
                string qText = "SELECT c.Nazv FROM Components c WHERE c.Type = " + type.Key;
                OleDbCommand Com = new OleDbCommand(qText, Con);
                OleDbDataReader reader = Com.ExecuteReader();
                while (reader.Read())
                {
                    switch (type.Value) {
                        case "cbCorpus":
                            cbCorpus.Items.Add(reader["Nazv"]);
                            break;
                        case "cbBoard":
                            cbBoard.Items.Add(reader["Nazv"]);
                            break;
                        case "cbCPU":
                            cbCPU.Items.Add(reader["Nazv"]);
                            break;
                        case "cbGraphic":
                            cbGraphic.Items.Add(reader["Nazv"]);
                            break;
                        case "cbOZU":
                            cbOZU.Items.Add(reader["Nazv"]);
                            break;
                        case "cbHDD":
                            cbHDD.Items.Add(reader["Nazv"]);
                            break;
                        case "cbSSD":
                            cbSSD.Items.Add(reader["Nazv"]);
                            break;
                        case "cbPower":
                            cbPower.Items.Add(reader["Nazv"]);
                            break;
                        case "cbDVD":
                            cbDVD.Items.Add(reader["Nazv"]);
                            break;
                        case "cbAudio":
                            cbAudio.Items.Add(reader["Nazv"]);
                            break;
                        case "cbIce":
                            cbIce.Items.Add(reader["Nazv"]);
                            break;
                    }                  
                }
            }
            Con.Close();
        }

        private int Summ(string naim) 
        {
            string qText = "SELECT c.Price FROM Components c WHERE c.Nazv = @naim";
            OleDbCommand Com = new OleDbCommand();
            Com.Parameters.AddWithValue("@naim", naim);
            Com.CommandText = qText;
            Com.Connection = Con;
            Con.Open();
                int price = Convert.ToInt32(Com.ExecuteScalar());
            Con.Close();            
            return price;
        }

        private string getComponentById(string id) 
        {
            string qText = "SELECT c.Nazv FROM Components c WHERE IDCOM = @id";
            OleDbCommand Com = new OleDbCommand(qText, Con);
            Com.Parameters.AddWithValue("@id", id);            
            string naim = Com.ExecuteScalar().ToString();
            return naim;
        }

        private string getCustomerById(string naim) 
        {
            string qText = "SELECT c.FIO FROM Customers c WHERE c.IDCUS = @id";
            OleDbCommand Com = new OleDbCommand(qText, Con);
            Com.Parameters.AddWithValue("@id", naim);
            string customer = Com.ExecuteScalar().ToString();
            return customer;
        }

        private int getIdComponent(string naim)
        {
            string qText = "SELECT c.IDCOM FROM Components c WHERE c.Nazv = @naim";
            OleDbCommand Com = new OleDbCommand();
            Com.Parameters.AddWithValue("@naim", naim);
            Com.CommandText = qText;
            Com.Connection = Con;
            Con.Open();
            int id = Convert.ToInt32(Com.ExecuteScalar());
            Con.Close();
            return id;
        }

        private int getIdCustomer(string naim)
        {
            string qText = "SELECT c.IDCUS FROM Customers c WHERE c.FIO = @naim";
            OleDbCommand Com = new OleDbCommand();
            Com.Parameters.AddWithValue("@naim", naim);
            Com.CommandText = qText;
            Com.Connection = Con;
            Con.Open();
            int id = (int)Com.ExecuteScalar();
            Con.Close();
            return id;
        }   

        private void cbCorpus_TextChanged(object sender, EventArgs e)
        {
            lPriceCorpus.Text = Summ(cbCorpus.Text).ToString();
        }

        private void cbBoard_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceBoard.Text = Summ(cbBoard.Text).ToString();
        }

        private void cbCPU_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceCPU.Text = Summ(cbCPU.Text).ToString();
        }

        private void cbGraphic_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceVideo.Text = Summ(cbGraphic.Text).ToString();
        }

        private void cbOZU_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceOZU.Text = Summ(cbOZU.Text).ToString();
        }

        private void cbHDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceHDD.Text = Summ(cbHDD.Text).ToString();
        }

        private void cbSSD_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceSSD.Text = Summ(cbSSD.Text).ToString();
        }

        private void cbPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPricePower.Text = Summ(cbPower.Text).ToString();
        }

        private void cbDVD_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceDVD.Text = Summ(cbDVD.Text).ToString();
        }

        private void cbAudio_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceAudio.Text = Summ(cbAudio.Text).ToString();
        }

        private void cbIce_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceIce.Text = Summ(cbIce.Text).ToString();
        }

        private int itogSumm()
        {
            int p = Convert.ToInt32(lPriceCorpus.Text) + Convert.ToInt32(lPriceBoard.Text) + Convert.ToInt32(lPriceCPU.Text) + Convert.ToInt32(lPriceVideo.Text) + Convert.ToInt32(lPriceOZU.Text) + Convert.ToInt32(lPriceHDD.Text) + Convert.ToInt32(lPriceSSD.Text) + Convert.ToInt32(lPricePower.Text) + Convert.ToInt32(lPriceDVD.Text) + Convert.ToInt32(lPriceAudio.Text) + Convert.ToInt32(lPriceIce.Text);
                return p;      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sprCustomerList sprCustomerListForm = new sprCustomerList();
            sprCustomerListForm.ModeCheck = "1";
            sprCustomerListForm.Owner = this;
            sprCustomerListForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lSumma.Text = itogSumm().ToString();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAssembly();
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveAssembly() 
        {
            try
            {
                OleDbCommand Com = new OleDbCommand();
                string qText = "";
                if (this.typeQuery == "edit")
                {
                    qText = "UPDATE Assembly SET IDCUS = @Customer, OrderDate = @OrderDate, Num = @Number, Summ = @Summ, Corpus = @Corpus, Board = @Board, CPU = @CPU, " +
                        " Graphic = @Graphic, OZU = @OZU, HDD = @HDD, SSD = @SSD, Power = @Power, DVD = @DVD, Audio = @Audio, Ice = @Ice";
                }
                else {
                    qText = "INSERT INTO Assembly (IDCUS, OrderDate, Num, Summ, Corpus, Board, CPU, Graphic, OZU, HDD, SSD, Power, DVD, Audio, Ice) " +
                " VALUES (@Customer, @OrderDate, @Number, @Summ, @Corpus, @Board, @CPU, @Graphic, @OZU, @HDD, @SSD, @Power, @DVD, @Audio, @Ice)";
                }                
                Com.Parameters.AddWithValue("@Customer", (int)getIdCustomer(tbCustomer.Text));
                Com.Parameters.AddWithValue("@OrderDate", Convert.ToDateTime(DateTime.Today));
                Com.Parameters.AddWithValue("@Number", tbNumber.Text);
                Com.Parameters.AddWithValue("@Summ", (int)itogSumm());
                Com.Parameters.AddWithValue("@Corpus", (int)getIdComponent(cbCorpus.Text));
                Com.Parameters.AddWithValue("@Board", (int)getIdComponent(cbBoard.Text));
                Com.Parameters.AddWithValue("@CPU", (int)getIdComponent(cbCPU.Text));
                Com.Parameters.AddWithValue("@Graphic", (int)getIdComponent(cbGraphic.Text));
                Com.Parameters.AddWithValue("@OZU", (int)getIdComponent(cbOZU.Text));
                Com.Parameters.AddWithValue("@HDD", (int)getIdComponent(cbHDD.Text));
                Com.Parameters.AddWithValue("@SSD", (int)getIdComponent(cbSSD.Text));
                Com.Parameters.AddWithValue("@Power", (int)getIdComponent(cbPower.Text));
                Com.Parameters.AddWithValue("@DVD", (int)getIdComponent(cbDVD.Text));
                Com.Parameters.AddWithValue("@Audio", (int)getIdComponent(cbAudio.Text));
                Com.Parameters.AddWithValue("@Ice", (int)getIdComponent(cbIce.Text));
                Com.CommandText = qText;
                Com.Connection = Con;
                Con.Open();
                Com.ExecuteNonQuery();
                Con.Close();
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
    }
}
