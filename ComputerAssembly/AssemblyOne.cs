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
    public partial class AssemblyOne : BaseForm
    {
        public AssemblyOne()
        {
            InitializeComponent();
            assemblyNumbers = new List<int?>();
        }

        string typeQuery;
        string idQuery;
        int _idAssembly;
        public int IdAssembly
        {
            get { return _idAssembly; }
            set { _idAssembly = value; }
        }
        int _idCustomer;
        public int IdCustomer
        {
            get { return _idCustomer; }
            set { _idCustomer = value; }
        }
        public List<int?> assemblyNumbers { get; set; }

        public string Customer 
        {
            set { tbCustomer.Text = value; }
            get { return tbCustomer.Text; }
        }
        Dictionary <int, string> arr = new Dictionary<int, string>();

        public string Type {
            set { typeQuery = value; }
        }

        public string ID
        {
            set { idQuery = value; }
            get { return idQuery; }
        }

        private async void AssemblyOne_Load(object sender, EventArgs e)
        {
             await loadComponents();
            if (this.typeQuery == "edit")
            {
                loadAssembly();
                lSumma.Text = itogSumm().ToString();
            }
        }

        public void saveElement(string text) 
        {
            string qText = text;
            OleDbCommand Com = new OleDbCommand();
        }

        private void loadAssembly()
        {
            try
            {
                var assembly = AssemblyBusinessLayer.FindAssemblyById(_idAssembly);
                cbCorpus.SelectedItem = GetSelectedItem(cbCorpus.Items, assembly.Corpus);
                cbBoard.SelectedItem = GetSelectedItem(cbBoard.Items, assembly.Board);
                cbCPU.SelectedItem = GetSelectedItem(cbCPU.Items, assembly.CPU);
                cbGraphic.SelectedItem = GetSelectedItem(cbGraphic.Items, assembly.Graphic);
                cbOZU.SelectedItem = GetSelectedItem(cbOZU.Items, assembly.OZU);
                cbHDD.SelectedItem = GetSelectedItem(cbHDD.Items, assembly.HDD);
                cbSSD.SelectedItem = GetSelectedItem(cbSSD.Items, assembly.SSD);
                cbPower.SelectedItem = GetSelectedItem(cbPower.Items, assembly.Power);
                cbDVD.SelectedItem = GetSelectedItem(cbDVD.Items, assembly.DVD);
                cbAudio.SelectedItem = GetSelectedItem(cbAudio.Items, assembly.Audio);
                cbIce.SelectedItem = GetSelectedItem(cbIce.Items, assembly.Ice);
                tbCustomer.Text = assembly.Customers.FIO;
                _idCustomer = assembly.Customers.IDCUS;
                tbNumber.Text = assembly.Num.ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }   
        }

        private ComponentsModel GetSelectedItem(System.Windows.Forms.ComboBox.ObjectCollection items, int? idComponent)
        {
            if (idComponent != null)
            {
                foreach (ComponentsModel item in items)
                {
                    if (item.IDCOM == idComponent)
                    {
                        return item;
                    }
                }
            }
            return new ComponentsModel();
        }

        private async Task<bool> loadComponents() 
        {
            arr.Add(1, "cbCorpus");
            arr.Add(2, "cbBoard");
            arr.Add(3, "cbCPU");
            arr.Add(4, "cbGraphic");
            arr.Add(5, "cbOZU");
            arr.Add(6, "cbHDD");
            arr.Add(7, "cbSSD");
            arr.Add(8, "cbPower");
            arr.Add(9, "cbDVD");
            arr.Add(10, "cbAudio");
            arr.Add(11, "cbIce");
            foreach(KeyValuePair<int, string> type in arr)
            {
                var componentsList = await AssemblyBusinessLayer.GetAllComponentsListAsync();
                if (componentsList != null)
                {
                    switch (type.Value)
                    {
                        case "cbCorpus":
                            var typeCorpusList = componentsList.Where(x => x.Type == type.Key);
                            foreach (var item in typeCorpusList)
                            {
                                cbCorpus.Items.Add(item);
                            }
                            break;
                        case "cbBoard":
                            var typeBoardList = componentsList.Where(x => x.Type == type.Key);
                            foreach (var item in typeBoardList)
                            {
                                cbBoard.Items.Add(item);
                            }
                            break;
                        case "cbCPU":
                            var typeCPUList = componentsList.Where(x => x.Type == type.Key);
                            foreach (var item in typeCPUList)
                            {
                                cbCPU.Items.Add(item);
                            }
                            break;
                        case "cbGraphic":
                            var typeGraphicList = componentsList.Where(x => x.Type == type.Key);
                            foreach (var item in typeGraphicList)
                            {
                                cbGraphic.Items.Add(item);
                            }
                            break;
                        case "cbOZU":
                            var typeOZUList = componentsList.Where(x => x.Type == type.Key);
                            foreach (var item in typeOZUList)
                            {
                                cbOZU.Items.Add(item);
                            }
                            break;
                        case "cbHDD":
                            var typeHDDList = componentsList.Where(x => x.Type == type.Key);
                            foreach (var item in typeHDDList)
                            {
                                cbHDD.Items.Add(item);
                            }
                            break;
                        case "cbSSD":
                            var typeSSDList = componentsList.Where(x => x.Type == type.Key);
                            foreach (var item in typeSSDList)
                            {
                                cbSSD.Items.Add(item);
                            }
                            break;
                        case "cbPower":
                            var typePowerList = componentsList.Where(x => x.Type == type.Key);
                            foreach (var item in typePowerList)
                            {
                                cbPower.Items.Add(item);
                            }
                            break;
                        case "cbDVD":
                            var typeDVDList = componentsList.Where(x => x.Type == type.Key);
                            foreach (var item in typeDVDList)
                            {
                                cbDVD.Items.Add(item);
                            }
                            break;
                        case "cbAudio":
                            var typeAudioList = componentsList.Where(x => x.Type == type.Key);
                            foreach (var item in typeAudioList)
                            {
                                cbAudio.Items.Add(item);
                            }
                            break;
                        case "cbIce":
                            var typeIceList = componentsList.Where(x => x.Type == type.Key);
                            foreach (var item in typeIceList)
                            {
                                cbIce.Items.Add(item);
                            }
                            break;
                    }
                }
            }
            return true;
        }

        private void cbCorpus_TextChanged(object sender, EventArgs e)
        {
            lPriceCorpus.Text = ((ComponentsModel)(cbCorpus.SelectedItem)).Price.ToString();
            cbCorpus.BackColor = ((ComponentsModel)(cbCorpus.SelectedItem)).Stock.InStock < 1 ? cbCorpus.BackColor = Color.Red : cbCorpus.BackColor = Color.White;
            lSumma.Text = itogSumm().ToString();
        }

        private void cbBoard_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceBoard.Text = ((ComponentsModel)(cbBoard.SelectedItem)).Price.ToString();
            cbBoard.BackColor = ((ComponentsModel)(cbBoard.SelectedItem)).Stock.InStock < 1 ? cbBoard.BackColor = Color.Red : cbBoard.BackColor = Color.White;
            lSumma.Text = itogSumm().ToString();
        }

        private void cbCPU_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceCPU.Text = ((ComponentsModel)(cbCPU.SelectedItem)).Price.ToString();
            cbCPU.BackColor = ((ComponentsModel)(cbCPU.SelectedItem)).Stock.InStock < 1 ? cbCPU.BackColor = Color.Red : cbCPU.BackColor = Color.White;
            lSumma.Text = itogSumm().ToString();
        }

        private void cbGraphic_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceVideo.Text = ((ComponentsModel)(cbGraphic.SelectedItem)).Price.ToString();
            cbGraphic.BackColor = ((ComponentsModel)(cbGraphic.SelectedItem)).Stock.InStock < 1 ? cbGraphic.BackColor = Color.Red : cbGraphic.BackColor = Color.White;
            lSumma.Text = itogSumm().ToString();
        }

        private void cbOZU_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceOZU.Text = ((ComponentsModel)(cbOZU.SelectedItem)).Price.ToString();
            cbOZU.BackColor = ((ComponentsModel)(cbOZU.SelectedItem)).Stock.InStock < 1 ? cbOZU.BackColor = Color.Red : cbOZU.BackColor = Color.White;
            lSumma.Text = itogSumm().ToString();
        }

        private void cbHDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceHDD.Text = ((ComponentsModel)(cbHDD.SelectedItem)).Price.ToString();
            cbHDD.BackColor = ((ComponentsModel)(cbHDD.SelectedItem)).Stock.InStock < 1 ? cbHDD.BackColor = Color.Red : cbHDD.BackColor = Color.White;
            lSumma.Text = itogSumm().ToString();
        }

        private void cbSSD_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceSSD.Text = ((ComponentsModel)(cbSSD.SelectedItem)).Price.ToString();
            cbSSD.BackColor = ((ComponentsModel)(cbSSD.SelectedItem)).Stock.InStock < 1 ? cbSSD.BackColor = Color.Red : cbSSD.BackColor = Color.White;
            lSumma.Text = itogSumm().ToString();
        }

        private void cbPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPricePower.Text = ((ComponentsModel)(cbPower.SelectedItem)).Price.ToString();
            cbPower.BackColor = ((ComponentsModel)(cbPower.SelectedItem)).Stock.InStock < 1 ? cbPower.BackColor = Color.Red : cbPower.BackColor = Color.White;
            lSumma.Text = itogSumm().ToString();
        }

        private void cbDVD_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceDVD.Text = ((ComponentsModel)(cbDVD.SelectedItem)).Price.ToString();
            cbDVD.BackColor = ((ComponentsModel)(cbDVD.SelectedItem)).Stock.InStock < 1 ? cbDVD.BackColor = Color.Red : cbDVD.BackColor = Color.White;
            lSumma.Text = itogSumm().ToString();
        }

        private void cbAudio_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceAudio.Text = ((ComponentsModel)(cbAudio.SelectedItem)).Price.ToString();
            cbAudio.BackColor = ((ComponentsModel)(cbAudio.SelectedItem)).Stock.InStock < 1 ? cbAudio.BackColor = Color.Red : cbAudio.BackColor = Color.White;
            lSumma.Text = itogSumm().ToString();
        }

        private void cbIce_SelectedIndexChanged(object sender, EventArgs e)
        {
            lPriceIce.Text = ((ComponentsModel)(cbIce.SelectedItem)).Price.ToString();
            cbIce.BackColor = ((ComponentsModel)(cbIce.SelectedItem)).Stock.InStock < 1 ? cbIce.BackColor = Color.Red : cbIce.BackColor = Color.White;
            lSumma.Text = itogSumm().ToString();
        }

        private decimal itogSumm()
        {
            decimal p = Convert.ToDecimal(lPriceCorpus.Text) + Convert.ToDecimal(lPriceBoard.Text) + Convert.ToDecimal(lPriceCPU.Text) + Convert.ToDecimal(lPriceVideo.Text) + Convert.ToDecimal(lPriceOZU.Text) + Convert.ToDecimal(lPriceHDD.Text) + Convert.ToDecimal(lPriceSSD.Text) + Convert.ToDecimal(lPricePower.Text) + Convert.ToDecimal(lPriceDVD.Text) + Convert.ToDecimal(lPriceAudio.Text) + Convert.ToDecimal(lPriceIce.Text);
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
            this.Close();
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveAssembly() 
        {
            try
            {
                if (_idAssembly > 0)
                {
                    AssemblyBusinessLayer.AddOrUpdateAssembly(_idAssembly, ((ComponentsModel)(cbAudio.SelectedItem)).IDCOM,
                        ((ComponentsModel)(cbBoard.SelectedItem)).IDCOM, ((ComponentsModel)(cbCorpus.SelectedItem)).IDCOM,
                        ((ComponentsModel)(cbCPU.SelectedItem)).IDCOM, null, ((ComponentsModel)(cbDVD.SelectedItem)).IDCOM,
                        ((ComponentsModel)(cbGraphic.SelectedItem)).IDCOM, ((ComponentsModel)(cbHDD.SelectedItem)).IDCOM,
                        ((ComponentsModel)(cbIce.SelectedItem)).IDCOM, _idCustomer, int.Parse(tbNumber.Text),
                        DateTime.Today, ((ComponentsModel)(cbOZU.SelectedItem)).IDCOM, ((ComponentsModel)(cbPower.SelectedItem)).IDCOM,
                        ((ComponentsModel)(cbSSD.SelectedItem)).IDCOM, 0, decimal.Parse(lSumma.Text));
                }
                else
                {
                    AssemblyBusinessLayer.AddOrUpdateAssembly(_idAssembly, ((ComponentsModel)(cbAudio.SelectedItem)).IDCOM,
                        ((ComponentsModel)(cbBoard.SelectedItem)).IDCOM, ((ComponentsModel)(cbCorpus.SelectedItem)).IDCOM,
                        ((ComponentsModel)(cbCPU.SelectedItem)).IDCOM, null, ((ComponentsModel)(cbDVD.SelectedItem)).IDCOM,
                        ((ComponentsModel)(cbGraphic.SelectedItem)).IDCOM, ((ComponentsModel)(cbHDD.SelectedItem)).IDCOM,
                        ((ComponentsModel)(cbIce.SelectedItem)).IDCOM, _idCustomer, int.Parse(tbNumber.Text),
                        DateTime.Today, ((ComponentsModel)(cbOZU.SelectedItem)).IDCOM, ((ComponentsModel)(cbPower.SelectedItem)).IDCOM,
                        ((ComponentsModel)(cbSSD.SelectedItem)).IDCOM, 0, decimal.Parse(lSumma.Text));
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }       
        }

        private void tbNumber_TextChanged(object sender, EventArgs e)
        {
            int number = 0;
            int.TryParse(tbNumber.Text, out number);
            var flag = assemblyNumbers.Contains(number);
            tbNumber.BackColor = !flag ? Color.White : Color.Red;
        }
    }
}
