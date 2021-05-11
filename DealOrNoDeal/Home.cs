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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
            this.Hide();
            InputName frmName = new InputName();
            frmName.ShowDialog();
            if (frmName.name != "")
            {
                Play frmPlay = new Play(frmName.name);
                frmPlay.ShowDialog();
            }
            this.Show();
        }
        private void btnAbout_Click(object sender, EventArgs e)
        {
            About frmAbout = new About();
            frmAbout.Show();
        }
        private void btnResults_Click(object sender, EventArgs e)
        {
            this.Hide();
            Results frmResults = new Results();
            frmResults.ShowDialog();
            this.Show();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
