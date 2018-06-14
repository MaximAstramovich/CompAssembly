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
    public partial class sprSuppliersOne : BaseForm
    {        
        string typeQuery;
        string idQuery;
        string Mode;
        SuppliersModel _currentSupplier;

        public string id
        {
            set { idQuery = value; }
            get { return idQuery; }
        }

        public string readMode {
            set { Mode = value; }
        }

        public string type
        {
            set
            {
                typeQuery = value;
            }
        }
            public sprSuppliersOne()
            {
                InitializeComponent();
            }

            private bool validate()
            {

                if (tbFIO.Text != "" || tbFirm.Text != "" || tbPosition.Text != "" || tbPhoneNumber.Text != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            private void sprSuppliersOne_Load(object sender, EventArgs e)
            {
                if (this.typeQuery == "edit")
                {
                    loadElement();
                }
            }

            private void loadElement()
            {
                try
                {
                    _currentSupplier = SuppliersBusinessLayer.FindSupplierById(int.Parse(id));
                    tbFIO.Text = _currentSupplier.FIO;
                    tbFirm.Text = _currentSupplier.Firm;
                    tbPosition.Text = _currentSupplier.Position;
                    tbPhoneNumber.Text = _currentSupplier.PhoneNumber.ToString();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }

            private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (validate())
                {
                    var phone = 1234567;
                    var isPhone = int.TryParse(tbPhoneNumber.Text, out phone);
                    int idSupplier = 0;
                    var flag = int.TryParse(id, out idSupplier);
                    if (flag)
                    {
                        SuppliersBusinessLayer.AddOrUpdateSupplier(idSupplier, _currentSupplier.Address, _currentSupplier.BankCode,
                            _currentSupplier.CheckingAccount, tbFIO.Text, tbFirm.Text, phone, tbPosition.Text, _currentSupplier.UNN);
                    }
                    else
                    {
                        SuppliersBusinessLayer.AddOrUpdateSupplier(idSupplier, string.Empty, string.Empty, 
                            0, tbFIO.Text, tbFirm.Text, phone, tbPosition.Text, 00000000);
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }

            private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
            {
                this.Close();
            }
    }
}
