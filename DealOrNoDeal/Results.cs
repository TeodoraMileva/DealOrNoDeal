using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DealOrNoDeal
{
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();
        }

        private List<Player> players = new List<Player>();

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            richTextBox.Text = "";
            richTextBoxSort.Text = "";
            lblHeading.Text = "";
            if (players.Count > 0)
            {
                players.Clear();
            }
            openFileDialog.Title = "Избор на текстов файл";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|Word documents|*.docx";
            if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileName.Length > 0)
            {
                string filename = "";
                string player = "";

                string name = "";
                double prize = 0;

                string[] data;
                char[] delimiterChars = { ' ', '-' };

                filename = openFileDialog.FileName;
                using (StreamReader reader = new StreamReader(filename))
                {
                    while (reader.Peek() >= 0)
                    {
                        player = reader.ReadLine();
                        data = player.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);

                        name = data[0];
                        prize = double.Parse(data[1]);

                        players.Add(new Player(name, prize));
                    }
                    for (int p = 0; p < players.Count; p++)
                    {
                        richTextBox.Text += players[p].Name + " - " + players[p].HighScore.ToString() + Environment.NewLine;
                    }
                }
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            if (players.Count < 1)
            {
                MessageBox.Show("Първо изберете файл!", "Грешка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                lblHeading.Text = "Сортиран списък:";
                richTextBoxSort.Text = "";
                for (int i = 0; i < players.Count - 1; i++)
                {
                    for (int j = i + 1; j < players.Count; j++)
                    {
                        if (players[i].HighScore < players[j].HighScore)
                        {
                            var tmp = players[i];
                            players[i] = players[j];
                            players[j] = tmp;
                        }
                    }
                }
                for (int k = 0; k < players.Count; k++)
                {
                    richTextBoxSort.Text += players[k].Name + " - " + players[k].HighScore.ToString() + Environment.NewLine;
                }
                for (int l = 0; l < 3; l++)
                {
                    richTextBoxSort.Select(richTextBoxSort.GetFirstCharIndexFromLine(l), richTextBoxSort.Lines[l].Length);
                    richTextBoxSort.SelectionFont = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                }
            }
        }

        private void btnTopPrize_Click(object sender, EventArgs e)
        {
            if (players.Count < 1)
            {
                MessageBox.Show("Първо изберете файл!", "Грешка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                lblHeading.Text = "Клуб 100,000:";
                richTextBoxSort.Text = "";

                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].HighScore == 100000)
                    {
                        richTextBoxSort.Text += players[i].Name + Environment.NewLine;
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
