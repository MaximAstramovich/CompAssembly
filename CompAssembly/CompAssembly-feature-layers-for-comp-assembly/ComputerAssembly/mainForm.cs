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
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AssemblyList AssemblyListForm = new AssemblyList();
            AssemblyListForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void комплектующиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sprAccessoryList sprAccessoryList = new sprAccessoryList();
            sprAccessoryList.Show();       
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sprCustomerList sprCustomerList = new sprCustomerList();
            sprCustomerList.Show();
        }

        private void поставщикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sprSuppliersList sprCounterpartyList = new sprSuppliersList();
            sprCounterpartyList.Show();
        }

        private void поставкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sprReceiptList sprReceiptList = new sprReceiptList();
            sprReceiptList.Show();
        }

        private void списокКомплектующихToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReportAccessory FormReportAccessory = new FormReportAccessory();
            FormReportAccessory.Show();
        }

        private void списокКлиентовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReportCustomer FormReportCustomer = new FormReportCustomer();
            FormReportCustomer.Show();
        }

        private void списокСборокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReportAssembly FormReportAssembly = new FormReportAssembly();
            FormReportAssembly.Show();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReference FormReference = new FormReference();
            FormReference.Show();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox AboutBox = new AboutBox();
            AboutBox.Show();
        }
    }
}
