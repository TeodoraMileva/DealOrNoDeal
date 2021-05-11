using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealOrNoDeal
{
    class Box
    {
        private int number;

        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        private double prize;

        public double Prize
        {
            get { return prize; }
            set { prize = value; }
        }
        private bool isOpened;

        public bool IsOpened
        {
            get { return isOpened; }
            set { isOpened = value; }
        }
        
        public Box ()
        {
            Number = 0;
            Prize = 0;
            isOpened = false;
        }
        public Box (int num, double pr)
        {
            Number = num;
            Prize = pr;
            isOpened = false;
        }
    }
}
