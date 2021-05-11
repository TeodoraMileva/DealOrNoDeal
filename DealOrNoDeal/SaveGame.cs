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
    public partial class SaveGame : Form
    {
        public SaveGame(string text)
        {
            InitializeComponent();
            lblMessage.Text = text;
        }
    }
}
