using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerAssembly.Extensions
{
    public static class ExtensionsClass
    {
        public static void SelectItemByValue(this System.Windows.Forms.ComboBox cbox, string value)
        {
            for (int i = 0; i < cbox.Items.Count; i++)
            {
                var prop = cbox.Items[i].GetType().GetProperty(cbox.ValueMember);
                if (prop != null && prop.GetValue(cbox.Items[i], null).ToString() == value)
                {
                    cbox.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
