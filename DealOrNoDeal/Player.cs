using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealOrNoDeal
{
    class Player
    {
        private string name;

        public  string Name
        {
            get { return name; }
            set { name = value; }
        }

        private double highScore;

        public double HighScore
        {
            get { return highScore; }
            set { highScore = value; }
        }
        
        public Player ()
        {
            Name = "";
            HighScore = 0;
        }
        
        public Player (string name, double highscore)
        {
            Name = name;
            HighScore = highscore;
        }
    }
}
