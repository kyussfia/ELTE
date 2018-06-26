﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWinFormsEVA
{
    public class Model
    {   
        private int [] rgb = new int[3];
        //property --> template: propfull + tab
        public int [] RGB     
        {
            get { return rgb; }
            set { rgb = value; }
        }

        public int[] ColorGenerator()
        {
            Random rnd = new Random();
            for(int i = 0; i < 3; i++)
            {
                RGB[i] = rnd.Next(0, 255);
            }
            return RGB;
        }
    }
}
