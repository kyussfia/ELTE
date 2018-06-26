using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleWinFormsEVA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Model model = new Model();

        bool isBusy = false;

        #region Synchronous    
        //regions are used to divide code 
        /*
        private int CountCharactersSync()
        {
            Thread.Sleep(5000);    //don't use this
            return 15;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (!isBusy)
            {
                isBusy = true;
                label1.Text = "Processing...";
                int count = CountCharactersSync();
                int[] color = model.ColorGenerator();
                panel1.BackColor = Color.FromArgb(color[0], color[1], color[2]);
                label1.Text = count.ToString();
                isBusy = false;
            }
        }*/
        
        #endregion

        #region Asynchronous
        //async operations are used for I/O methods or CPU-bound code like expensive calculations
        private async Task<int> CountCharacters()
        {
            int count = 15;
            await Task.Delay(5000);   //use Task.Delay if waiting is necessary
            return count;
        }

        //event handler for button
        //add in Design/Properties/Events or use button1.Click += new EventHandler(btn1_Click)
        //do not add multiple event handlers to UI element at once
        //only use async void in signature of event handlers
        private async void btn1_Click(object sender, EventArgs e)
        {
            if (!isBusy)
            {
                isBusy = true;
                label1.Text = "Processing...";
                int count = await CountCharacters();   //async and await go hand in hand, do not use one without the other
                int[] color = model.ColorGenerator();
                panel1.BackColor = Color.FromArgb(color[0], color[1], color[2]);
                label1.Text = count.ToString();
                isBusy = false;
            }
        }
        
        #endregion
    }
}
