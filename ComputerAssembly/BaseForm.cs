using BL;
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
    public partial class BaseForm : Form
    {
        public CustomersBLL CustomersBusinessLayer;
        public SuppliersBLL SuppliersBusinessLayer;
        public ReceiptsBLL ReceiptsBusinessLayer;
        public ComponentsBLL ComponentsBusinessLayer;
        public AssemblyBLL AssemblyBusinessLayer;
        public BaseForm()
        {
            InitializeComponent();
            CustomersBusinessLayer = new CustomersBLL();
            SuppliersBusinessLayer = new SuppliersBLL();
            ReceiptsBusinessLayer = new ReceiptsBLL();
            ComponentsBusinessLayer = new ComponentsBLL();
            AssemblyBusinessLayer = new AssemblyBLL();
        }
    }
}
