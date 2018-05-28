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
    public partial class FormReportAccessory : Form
    {
        OleDbConnection Con = new OleDbConnection();
        public FormReportAccessory()
        {
            InitializeComponent();
            Con.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=DB.mdb;Persist Security Info=False;";
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT Components.Type, Components.Nazv, Components.Price FROM Components;", Con);
            DataSetAccessory ds = new DataSetAccessory();
            da.Fill(ds, "DataTable1");

            ReportDocument rDoc = new ReportDocument();
            rDoc.Load("CrystalReportAccessory.rpt");
            rDoc.SetDataSource(ds);
            crystalReportViewer1.ReportSource = rDoc; 
        }
    }
}
