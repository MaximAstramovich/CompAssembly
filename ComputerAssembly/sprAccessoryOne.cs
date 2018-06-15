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
    public partial class sprAccessoryOne : BaseForm
    {
        public sprAccessoryOne()
        {
            InitializeComponent();
            _componentTypesList = new List<ComponentTypesModel>();
        }

        string typeQuery;
        string idQuery;
        string Mode;
        
        ComponentsModel _currentComponent;
        List<ComponentTypesModel> _componentTypesList;

        int _lastComponentId = 0;
        public int LastComponentId
        {
            get { return _lastComponentId; }
            set { _lastComponentId = value; }
        }
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

        private async void sprAccessoryOne_Load(object sender, EventArgs e)
        {
            try {
                await LoadComponentTypesList();
                if (this.typeQuery == "edit")
                {
                    loadElement();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void loadElement()
        {
            try
            {
                _currentComponent = ComponentsBusinessLayer.FindComponentById(int.Parse(id));
                tbName.Text = _currentComponent.Nazv;
                tbPrice.Text = _currentComponent.Price.Value.ToString();
                rtbDescription.Text = _currentComponent.Description;
                cbType.SelectedItem = _componentTypesList.Find(x => x.ID == _currentComponent.Type).Type;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private async Task LoadComponentTypesList()
        {
            _componentTypesList.AddRange(await ComponentsBusinessLayer.GetAllComponentTypesListAsync());
            foreach (var type in _componentTypesList)
            {
                cbType.Items.Add(type.Type);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
                if (tbName.Text == "" || cbType.Text == "" || tbPrice.Text == "")
                {
                    MessageBox.Show("Заполните необходимые поля");
                }
                else 
                {
                    decimal price = 0;
                    var isPrice = decimal.TryParse(tbPrice.Text, out price);
                    int idComponent = _lastComponentId;
                    int type = 0;
                    type = _componentTypesList.FirstOrDefault(x => x.Type == cbType.SelectedItem.ToString()).ID;
                    var flag = int.TryParse(id, out idComponent);
                    if (flag)
                    {
                        ComponentsBusinessLayer.AddOrUpdateComponent(idComponent, rtbDescription.Text, price, type, tbName.Text);
                    }
                    else
                    {
                        idComponent = ++_lastComponentId;
                        ComponentsBusinessLayer.AddOrUpdateComponent(idComponent, rtbDescription.Text, price, type, tbName.Text);
                    }
                    this.Close();
                }
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
