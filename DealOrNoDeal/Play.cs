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
    public partial class Play : Form
    {
        public Play(string namePlayer)
        {
            InitializeComponent();

            this.Text = "Сделка или не | Играч - " + namePlayer;
            name = namePlayer;

            BoxGenerator();

            briefcases = new List<PictureBox>()
            {
                pictureBox0, pictureBox1, pictureBox2, pictureBox3,
                pictureBox4, pictureBox5, pictureBox6, pictureBox7,
                pictureBox8, pictureBox9, pictureBox10, pictureBox11,
                pictureBox12, pictureBox13, pictureBox14, pictureBox15
            };

            labelsPrize = new List<Label>()
            {
                lblPrize0, lblPrize1, lblPrize2, lblPrize3, lblPrize4,
                lblPrize5, lblPrize6, lblPrize7, lblPrize8, lblPrize9,
                lblPrize10, lblPrize11, lblPrize12, lblPrize13,
                lblPrize14, lblPrize15
            };

            for (int i = 8; i < labelsPrize.Count; i++)
            {
                labelsPrize[i].Text = String.Format("{0:#,0}", boxPrizesConst[i]);
            }
        }

        string name = "";
        double prize = 0;

        private List<PictureBox> briefcases;
        private List<Label> labelsPrize;


        private List<double> boxPrizesTemp = new List<double>()
        {
            0.1, 1, 5, 10, 50, 100, 300, 500,
            1000, 2500, 5000, 10000, 15000, 20000, 50000, 100000
        };

        private List<double> boxPrizesConst = new List<double>()
        {
            0.1, 1, 5, 10, 50, 100, 300, 500,
            1000, 2500, 5000, 10000, 15000, 20000, 50000, 100000
        };
        private List<Box> boxes = new List<Box>();

        string[] messages =
        {
            "Отворете 3 кутии.",
            "Отворете 2 кутии.",
            "Отворете 1 кутия."
        };

        int countMessage = 0;
                
        int myBoxIndex = -1;
        int selectedBoxIndex = -1;

        public void BoxGenerator()
        {
            //Put the prizes in the boxes in a random order
            Random rnd = new Random();
            int index = 0;
            for (int i = 0; i < 16; i++)
            {
                index = rnd.Next(0, boxPrizesTemp.Count);
                Box tmp = new Box(i, boxPrizesTemp[index]);
                boxPrizesTemp.RemoveAt(index);
                boxes.Add(tmp);
            }
        }
        public void HandleBriefcaseMouseEnter (object sender, EventArgs e)
        {
            Control pb = (Control)sender;
            int index = int.Parse(pb.Tag.ToString());
            for (int i = 0; i < briefcases.Count; i++)
            {
                if (i != myBoxIndex && i != selectedBoxIndex && i != index)
                {
                    briefcases[i].BorderStyle = BorderStyle.None;
                }
            }
            if (index != myBoxIndex)
            {
                briefcases[index].BorderStyle = BorderStyle.FixedSingle;
            }
            
        }
        public void HandleBriefcaseMouseLeave (object sender, EventArgs e)
        {
            Control pb = (Control)sender;
            int index = int.Parse(pb.Tag.ToString());
            if (index != myBoxIndex && index != selectedBoxIndex)
            {
                briefcases[index].BorderStyle = BorderStyle.None;
            }
            for (int i = 0; i < briefcases.Count; i++)
            {
                if (i != myBoxIndex && i != selectedBoxIndex)
                {
                    briefcases[i].BorderStyle = BorderStyle.None;
                }
            }
        }
        public void HandleMyBriefcaseClick (object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            if (myBoxIndex == -1)
            {
                myBoxIndex = int.Parse(ctrl.Tag.ToString());
                briefcases[myBoxIndex].BorderStyle = BorderStyle.Fixed3D;
                lblMessage.Text = MessageGenerator();
            }
            else
            {
                selectedBoxIndex = int.Parse(ctrl.Tag.ToString());

                if (selectedBoxIndex != myBoxIndex)
                {
                    briefcases[selectedBoxIndex].BorderStyle = BorderStyle.FixedSingle;
                    for (int i = 0; i < briefcases.Count; i++)
                    {
                        if (i != selectedBoxIndex && i != myBoxIndex)
                        {
                            briefcases[i].BorderStyle = BorderStyle.None;
                        }
                    }
                }
            }
        }

        public void btnOpenBox_Click(object sender, EventArgs e)
        {
            if (selectedBoxIndex == -1)
            {
                if (myBoxIndex == -1 )
                {
                    MessageBox.Show("Изберете своята кутия преди\nда отворите друга кутия!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Изберете кутия!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                
            }
            else if (selectedBoxIndex == myBoxIndex)
            {
                MessageBox.Show("Не може да отворите тази кутия.\nИзберете друга кутия!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (boxes[selectedBoxIndex].IsOpened == true)
                {
                    MessageBox.Show("Вече отворихте тази кутия.\nИзберете друга кутия!", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    briefcases[selectedBoxIndex].Image = Properties.Resources.opened_no_bg1;
                    briefcases[selectedBoxIndex].BorderStyle = BorderStyle.None;
                    briefcases[selectedBoxIndex].Enabled = false;

                    double pr = boxes[selectedBoxIndex].Prize;

                    for (int i = 0; i < labelsPrize.Count(); i++)
                    {
                        if (boxPrizesConst[i] == pr)
                        {
                            labelsPrize[i].BackColor = Color.FromArgb(224, 224, 224);
                            break;
                        }
                    }

                    string prize = "";

                    if (pr >= 1000)
                    {
                        prize = String.Format("{0:#,0}", pr);
                    }
                    else
                    {
                        prize = pr.ToString();
                    }

                    MessageBox.Show(prize + " лв.", "Кутия " + (selectedBoxIndex + 1).ToString());
                    
                    lblMessage.Text = MessageGenerator();

                    boxes[selectedBoxIndex].IsOpened = true;
                    selectedBoxIndex = -1;

                    int count = 0;
                    for (int i = 0; i < 16; i++)
                    {
                        if (boxes[i].IsOpened == false)
                        {
                            count++;
                        }
                    }

                    if (count == 1)
                    {
                        GameExit();
                    }
                }
            }
        }

        public double AverageSum ()
        {
            int counter = 0;
            double sum = 0;
            for (int i = 0; i < boxes.Count; i++)
            {
                if (boxes[i].IsOpened == false)
                {
                    counter++;
                    sum += boxes[i].Prize;
                }
            }
            return Math.Round(sum / counter, 2);
        }

        public string MessageGenerator ()
        {
            switch (countMessage)
            {
                case 0:
                    countMessage++;
                    return "Вие избрахте кутия № " + (myBoxIndex + 1) + ". " + messages[0];
                case 1:
                    countMessage++;
                    return messages[1];
                case 2:
                    countMessage++;
                    return messages[2];
                case 3:
                    countMessage++;
                    OfferMode();
                    return "Офертата на банката е: " + String.Format("{0:#,0}", AverageSum()) + " лв.";
                case 4:
                    countMessage++;
                    return messages[0];
                case 5:
                    countMessage++;
                    return messages[1];
                case 6:
                    countMessage++;
                    return messages[2];
                case 7:
                    countMessage++;
                    OfferMode();
                    return "Офертата на банката е: " + String.Format("{0:#,0}", AverageSum()) + " лв.";
                case 8:
                    countMessage++;
                    return messages[0];
                case 9:
                    countMessage++;
                    return messages[1];
                case 10:
                    countMessage++;
                    return messages[2];
                case 11:
                    countMessage++;
                    OfferMode();
                    return "Офертата на банката е: " + String.Format("{0:#,0}", AverageSum()) + " лв.";
                case 12:
                    countMessage++;
                    return messages[1];
                case 13:
                    countMessage++;
                    return messages[2];
                case 14:
                    countMessage++;
                    OfferMode();
                    return "Офертата на банката е: " + String.Format("{0:#,0}", AverageSum()) + " лв.";
                case 15:
                    countMessage++;
                    return messages[1];
                case 16:
                    countMessage++;
                    return messages[2];
                case 17:
                    countMessage++;
                    OfferMode();
                    return "Офертата на банката е: " + String.Format("{0:#,0}", AverageSum()) + " лв.";
                case 18:
                    countMessage++;
                    return messages[2];
                case 19:
                    countMessage++;
                    OfferMode();
                    return "Офертата на банката е: Смяна на кутиите";
                default:
                    return "";
            }
        }
        public void OfferMode ()
        {
            btnOpenBox.Hide();
            btnDeal.Show();
            btnNoDeal.Show();
            for (int i = 0; i < 16; i++)
            {
                briefcases[i].Enabled = false;
            }
        }

        public void btnDeal_Click(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i < 16; i++)
            {
                if (boxes[i].IsOpened == false)
                {
                    count++;
                    if (i != myBoxIndex)
                    {
                        selectedBoxIndex = i;
                    }
                }
            }
            if (count == 2)
            {
                briefcases[selectedBoxIndex].BorderStyle = BorderStyle.Fixed3D;
                briefcases[myBoxIndex].BorderStyle = BorderStyle.None;
                int temp = selectedBoxIndex;
                selectedBoxIndex = myBoxIndex;
                myBoxIndex = temp;
                for (int i = 0; i < 16; i++)
                {
                    if (boxes[i].IsOpened == false)
                    {
                        briefcases[i].Enabled = true;
                    }
                }
                btnOpenBox.Show();
                btnDeal.Hide();
                btnNoDeal.Hide();
            }
            else
            {
                GameExit();
            }
            
        }

        public void btnNoDeal_Click(object sender, EventArgs e)
        {
            btnOpenBox.Show();
            btnDeal.Hide();
            btnNoDeal.Hide();
            lblMessage.Text = MessageGenerator();
            for (int i = 0; i < 16; i++)
            {
                if (!boxes[i].IsOpened)
                {
                    briefcases[i].Enabled = true;
                }
            }
        }
        public void GameExit ()
        {
            int count = 0;
            for (int i = 0; i < 16; i++)
            {
                if (boxes[i].IsOpened == false)
                {
                    count++;
                }
            }
            this.Hide();
            SaveGame frmSaveGame;
            DialogResult result;

            string message = "";

            if (count != 1)
            {
                if (AverageSum() >= 1000)
                {
                    message = String.Format("{0:#,0}", AverageSum());
                }
                else
                {
                    message = AverageSum().ToString();
                }
            }
            else
            {
                if (boxes[myBoxIndex].Prize >= 1000)
                {
                    message = String.Format("{0:#,0}", boxes[myBoxIndex].Prize);
                }
                else
                {
                    message = boxes[myBoxIndex].Prize.ToString();
                }
            }

            frmSaveGame = new SaveGame("Вашата печалба е: " + message + " лв.\nИскате ли да запишете тази игра?");

            result = frmSaveGame.ShowDialog();
            if (result == DialogResult.Yes)
            {
                string filename = "";
                openFileDialog.Title = "Избор на текстов файл";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|Word documents|*.docx";
                if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileName.Length > 0)
                {
                    filename = openFileDialog.FileName;
                    using (StreamWriter writer = new StreamWriter(filename, true))
                    {
                        if (count != 1)
                        {
                            prize = AverageSum();
                        }
                        else
                        {
                            prize = boxes[myBoxIndex].Prize;
                        }
                        writer.WriteLine(name + " - " + prize.ToString());
                        frmSaveGame.Close();
                    }
                }
            }
            else if (result == DialogResult.No)
            {
                frmSaveGame.Close();
            }
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            EarlyExit frmExit = new EarlyExit();
            DialogResult result = frmExit.ShowDialog();
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}