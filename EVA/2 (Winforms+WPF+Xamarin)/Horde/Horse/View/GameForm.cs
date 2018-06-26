using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Horse.Model;
using System.Diagnostics;

namespace Horse
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
        }

        private HorseGameModel model;
        private Button[,] buttonGrid;
        private Timer timer;
        private int pauseClicks;

        private void Game_GameOver(Object sender, HorseEventArgs e)
        {
            foreach (Button b in buttonGrid)
                b.Enabled = false;
            timer.Stop();

            MessageBox.Show("Gratulálok, győztél!" + Environment.NewLine +
                                "Összesen " + e.Score + " pontot szereztél! ",
                                "Bejárás huszárral",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
            model.NewGame(model.Size);
            GenerateTable(model.Size);
            Score.Text = model.Score.ToString();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        public void GameForm_Load(Object sender, EventArgs e)
        {
            model = new HorseGameModel();
            model.GameOver += new EventHandler<HorseEventArgs>(Game_GameOver);
            model.StepReload += new EventHandler<HorseEventArgs>(Game_StepReload);
            model.StepBack += new EventHandler<HorseEventArgs>(Game_StepBack);
            model.GameAdvanced += new EventHandler<HorseEventArgs>(Game_GameAdvanced);
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(Timer_Tick);
            pauseClicks = 0;
            GenerateTable(3);
            model.NewGame(3);
            timer.Start();
        }

        #region Menu event handlers

        private void MenuGameEasy_Click(Object sender, EventArgs e)
        {
            model.NewGame(3);
            GenerateTable(3);
            Score.Text = model.Score.ToString();
        }

        private void MenuGameMedium_Click(Object sender, EventArgs e)
        {
            model.NewGame(4);
            GenerateTable(4);
            Score.Text = model.Score.ToString();
        }

        private void MenuGameHard_Click(Object sender, EventArgs e)
        {
            model.NewGame(6);
            GenerateTable(6);
            Score.Text = model.Score.ToString();
        }

        private void Pause_Click(Object sender, EventArgs e)
        {
            pauseClicks++;
            if (pauseClicks % 2 == 1)
            {
                foreach (Button b in buttonGrid)
                    b.Enabled = false;
                timer.Stop();
            }
            else
            {
                foreach (Button b in buttonGrid)
                    b.Enabled = true;
                timer.Start();
            }
        }

        #endregion

        public void GenerateTable(int size)
        {
            if (buttonGrid != null)
            {
                for (int i = 0; i < buttonGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < buttonGrid.GetLength(0); j++)
                    {
                        Controls.Remove(buttonGrid[i, j]);
                    }
                }
            }

            buttonGrid = new Button[size, size];
            int x = 30;
            int y = 50;
            for (Int32 i = 0; i < size; i++)
            {
                for (Int32 j = 0; j < size; j++)
                {
                    buttonGrid[i, j] = new Button();
                    buttonGrid[i, j].Location = new Point(x, y); // elhelyezkedés
                    buttonGrid[i, j].Size = new Size(40, 40); // méret
                    buttonGrid[i, j].Enabled = true; // kikapcsolt állapot
                    buttonGrid[i, j].TabIndex = 100 + i * model.Size + j; // a gomb számát a TabIndex-ben tároljuk
                    if ((i + j) % 2 == 0)
                        buttonGrid[i, j].BackColor = Color.White;
                    else
                        buttonGrid[i, j].BackColor = Color.Black;
                    buttonGrid[i, j].MouseClick += new MouseEventHandler(ButtonClick);

                    Controls.Add(buttonGrid[i, j]);
                    y += 40;
                }
                x += 40;
                y = 50;
            }
            buttonGrid[0, 0].BackColor = Color.Yellow;
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            int x = ((sender as Button).TabIndex - 100) / model.Size;
            int y = ((sender as Button).TabIndex - 100) % model.Size;
            model.Step(x, y);
            
        }

        private void Game_StepReload(Object sender, HorseEventArgs e)
        {
            if((e.lastX + e.lastY) % 2 == 0)
            {
                buttonGrid[e.lastX, e.lastY].BackColor = Color.LightBlue;
            }
            else
            {
                buttonGrid[e.lastX, e.lastY].BackColor = Color.Navy;
            }
            buttonGrid[e.X, e.Y].BackColor = Color.Yellow;
            Score.Text = model.Score.ToString();
        }

        private void Game_StepBack(Object sender, HorseEventArgs e)
        {
            if((e.backX + e.backY) % 2 == 0)
            {
                buttonGrid[e.backX, e.backY].BackColor = Color.White;
            }
            else
            {
                buttonGrid[e.backX, e.backY].BackColor = Color.Black;
            }
        }

        private void Timer_Tick(Object sender, EventArgs e)
        {
            model.AdvanceTime();
        }

        private void Game_GameAdvanced(Object sender, HorseEventArgs e)
        {
            TimeLabel.Text = TimeSpan.FromSeconds(e.GameTime).ToString("g");
        }
    }
}
