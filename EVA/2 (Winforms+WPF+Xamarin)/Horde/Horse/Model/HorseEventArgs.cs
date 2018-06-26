using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horse.Model
{
    public class HorseEventArgs : EventArgs
    {
        public Int32 X { get; private set; }

        public Int32 Y { get; private set; }

        public Int32 lastX { get; private set; }

        public Int32 lastY { get; private set; }

        public int backX { get; private set; }

        public int backY { get; private set; }

        private Int32 gametime;
        public int GameTime { get { return gametime; } }

        public HorseEventArgs(Int32 time, bool dummy)
        {
            gametime = time;
        }

        public HorseEventArgs(Int32 x, Int32 y, Int32 lx, Int32 ly)
        {
            X = x;
            Y = y;
            lastX = lx;
            lastY = ly;
        }

        public int Score { get; private set; }
        
        public HorseEventArgs(int sc)
        {
            Score = sc;
        }
        
        public HorseEventArgs(int x, int y)
        {
            backX = x;
            backY = y;
        } 
    }
}
