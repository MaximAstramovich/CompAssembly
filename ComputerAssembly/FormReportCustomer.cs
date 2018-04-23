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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ComputerAssembly
{
    public partial class FormReportCustomer : Form
    {
        OleDbConnection Con = new OleDbConnection();
        public FormReportCustomer()
        {
            InitializeComponent();
            Con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=DB.mdb;Persist Security Info=False;";
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT Customers.FIO, Customers.Address, Customers.PhoneNumber FROM Customers;", Con);
            DataSetCustomer ds = new DataSetCustomer();
            da.Fill(ds, "DataTable1");

            ReportDocument rDoc = new ReportDocument();
            rDoc.Load("CrystalReportCustomer.rpt");
            rDoc.SetDataSource(ds);
            crystalReportViewer1.ReportSource = rDoc; 
        }
    }
}
