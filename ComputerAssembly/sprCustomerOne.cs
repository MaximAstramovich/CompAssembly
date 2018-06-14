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
using DAL.DBModel;
using DAL.Models;

namespace ComputerAssembly
{
    public partial class sprCustomerOne : BaseForm
    {        
        string typeQuery;
        string idQuery;
        string Mode;

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
        public sprCustomerOne()
        {
            InitializeComponent();
        }

        private bool validate() {

            if (tbFIO.Text != "" || tbAddress.Text != "" || tbAuthority.Text != "" || tbPassportNo.Text != "" || tbPhoneNumber.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void sprCustomerOne_Load(object sender, EventArgs e)
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
                var customer = CustomersBusinessLayer.FindCustomerById(int.Parse(id));
                tbFIO.Text = customer.FIO;
                dtDateOfBirth.Text = customer.DateOfBirth.ToString();
                tbPassportNo.Text = customer.PassportNo;
                dtDateOfIssue.Text = customer.DateOfIssue.ToString();
                tbAuthority.Text = customer.Authority;
                tbAddress.Text = customer.Address;
                tbPhoneNumber.Text = customer.PhoneNumber.ToString();
            }
            catch (Exception err) {
                MessageBox.Show(err.Message);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                var phone = 1234567;
                var isPhone = int.TryParse(tbPhoneNumber.Text, out phone);
                int idCustomer = 0;
                var flag = int.TryParse(id, out idCustomer);
                if (flag)
                {
                    CustomersBusinessLayer.AddOrUpdateCustomer(idCustomer, tbAddress.Text, tbAuthority.Text, Convert.ToDateTime(dtDateOfBirth.Text),
                                                               Convert.ToDateTime(dtDateOfIssue.Text), tbFIO.Text,
                                                               tbPassportNo.Text, phone);
                }
                else
                {
                    CustomersBusinessLayer.AddOrUpdateCustomer(idCustomer, tbAddress.Text, tbAuthority.Text, Convert.ToDateTime(dtDateOfBirth.Text),
                                                               Convert.ToDateTime(dtDateOfIssue.Text), tbFIO.Text,
                                                               tbPassportNo.Text, phone);
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
