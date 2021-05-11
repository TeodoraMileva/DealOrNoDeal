using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DealOrNoDeal
{
    public partial class InputName : Form
    {
        public InputName()
        {
            InitializeComponent();
        }
        public string name = "";
        private void tbName_TextChanged(object sender, EventArgs e)
        {
            if (tbName.Text == "")
            {
                errorProvider.SetError(tbName, "Въведете име!");
                tbName.Focus();
            }
            else
            {
                errorProvider.SetError(tbName, "");
            }
        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "")
            {
                errorProvider.SetError(tbName, "Въведете име!");
                tbName.Focus();
            }
            else
            {
                name = tbName.Text;
                this.Close();
            }
        }
    }
}
