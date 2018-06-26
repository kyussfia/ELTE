using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LightDuel_WinForms.Model;

using System.Diagnostics;

namespace LightDuel_WinForms.View
{
    public partial class MainWindow : Form
    {
        #region Clock class

        private partial class Clock : Object
        {
            public int hour = 0;
            public int min = 0;
            public int sec = 0;

            public string getLabel()
            {
                return (this.hour < 10 ? "0" + this.hour.ToString() : this.hour.ToString()) + ":" +
                    (this.min < 10 ? "0" + this.min.ToString() : this.min.ToString()) + ":" +
                    (this.sec < 10 ? "0" + this.sec.ToString() : this.sec.ToString());
            }

            public void ticked()
            {
                this.sec = this.sec + 1;
                if (this.sec == 60)
                {
                    this.sec = 0;
                    this.min = this.min + 1;
                    if (this.min == 60)
                    {
                        this.min = 0;
                        this.hour = this.hour + 1;
                    }
                }
            }

            public void reset()
            {
                this.sec = 0;
                this.min = 0;
                this.hour = 0;
            }
        }
        #endregion

        #region Private Fields 

        private LightDuelModel model;   

        private Clock clock;

        private event EventHandler<PlayerMoveEventArgs> tickHandler;

        private event EventHandler<GameOverEventArgs> gameOverHandler;

        private event MouseEventHandler clickHandler;

        private Button[,] buttonGrid;

        private int periodCounter;

        private int TimePeriod;

        private bool disableKeys;

        private bool isPaused;

        private int P1Left = 65; //key A

        private int P1Right = 68; //key D

        private int P2Left = 37; //key left arrow

        private int P2Right = 39; // key right arrow

        #endregion

        #region Init methods

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.clock = new Clock();
            this.periodCounter = 0;
            this.model = new LightDuelModel();
            this.TimePeriod = (1000 / model.speed) - 1;
            this.disableKeys = true;
            this.isPaused = true;
            this.KeyPreview = true;

            this.tickHandler = new EventHandler<PlayerMoveEventArgs>(clockTicked);
            this.clickHandler = new MouseEventHandler(pauseClicked);
            this.gameOverHandler = new EventHandler<GameOverEventArgs>(gameOver);
        }

        #endregion

        #region Explicit View events

        private void közepesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startGame(24); //24     
        }

        private void kicsiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startGame(12); //12
        }

        private void nagyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startGame(36);
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (!disableKeys)
            {
                if (e.KeyValue == P1Left)
                {
                         this.model.Blue.left();
                }

                if (e.KeyValue == P1Right)
                {
                    this.model.Blue.right();
                }

                if (e.KeyValue == P2Left)
                {
                    this.model.Red.left();
                }

                if (e.KeyValue == P2Right)
                {
                    this.model.Red.right();
                }
            }           
        }

        private void pauseClicked(Object sender, EventArgs e)
        {
            this.model.handlePausing(isPaused);
            this.isPaused = isPaused ? false : true;
            this.Pause.Text = isPaused ? "Start" : "Pause";
            this.disableKeys = isPaused ? true : false;
        }

        #endregion

        #region Model events

        private void clockTicked(object sender, PlayerMoveEventArgs e)
        {
            if (periodCounter == TimePeriod)
            {
                this.clock.ticked();
                this.clockLabel.Text = this.clock.getLabel();
                periodCounter = 0;
            } else
            {
                periodCounter++;
            }

            //coloring new positions:
            buttonGrid[e.BlueX, e.BlueY].BackColor = GetBrushFor(e.BlueX, e.BlueY);
            buttonGrid[e.RedX, e.RedY].BackColor = GetBrushFor(e.RedX, e.RedY);
        }

        private void gameOver(object sender, GameOverEventArgs e)
        {
            this.clearGame();
            if (e.BlueLost && e.RedLost)
            {
                this.tied();
            } else if (e.BlueLost)
            {
                this.blueLost();
            } else if (e.RedLost)
            {
                this.redLost();
            }
        }

        #endregion

        #region Private View Methods

        private void clearFields()
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
        }

        private void buildFields()
        {
            this.clearFields();
            
            buttonGrid = new Button[model.GameSize, model.GameSize];

            int buttonHeight = 27;

            int buttonSize = 850 / model.GameSize;
            int x = 0;
            int y = 65 + buttonHeight;
            for (Int32 i = 0; i < model.GameSize; i++)
            {
                for (Int32 j = 0; j < model.GameSize; j++)
                {
                    buttonGrid[i, j] = new Button();
                    buttonGrid[i, j].Location = new Point(x, y); // elhelyezkedés
                    buttonGrid[i, j].Size = new Size(buttonSize, buttonSize); // méret
                    buttonGrid[i, j].Enabled = false; // kikapcsolt állapot
                    buttonGrid[i, j].TabIndex = 100 + i * model.GameSize + j; // a gomb számát a TabIndex-ben tároljuk
                    buttonGrid[i, j].BackColor = this.GetBrushFor(i, j);

                    Controls.Add(buttonGrid[i, j]);
                    y += buttonSize;
                }
                x += buttonSize;
                y = 65 + buttonHeight;
            }
        }

        private Color GetBrushFor(int col, int row)
        {
            if (this.model.isBluePosition(col, row))
            {
                return Color.Blue;
            } else if(this.model.isRedPosition(col, row))
            {
                return Color.Red;
            } else
            {
                return Color.Transparent;
            }        
        }

        private void clearGame()
        {
            this.periodCounter = 0;  
            this.model.ticked -= this.tickHandler;
            this.model.gameOver -= this.gameOverHandler;
            this.Pause.MouseClick -= this.clickHandler;
            this.clock.reset();
            this.disableKeys = false;
            this.isPaused = false;
        }

        #endregion

        #region Game Methods

        private void startGame(int size)
        {
            this.clearGame();
            this.clearFields();

            //reset
            this.clockLabel.Text = this.clock.getLabel();
            this.model.ticked += this.tickHandler;
            this.model.gameOver += this.gameOverHandler;
            this.model.newGame(size);
            this.Pause.MouseClick += this.clickHandler;

            buildFields();
        }

        private void tied()
        {
            MessageBox.Show("Döntetlen" + Environment.NewLine +
                                " Elért idő:  " + this.clock.getLabel(),
                                "Light-Duel",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
        }

        private void playerLost(bool isBlue = true)
        {
            MessageBox.Show("A győztes: " + Environment.NewLine +
                                (isBlue ? "Piros játékos" : "Kék játékos") +
                                " Elért idő:  " + this.clock.getLabel(),
                                "Light-Duel - Győzelem",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
        }

        private void blueLost()
        {
            playerLost(true);
        }

        private void redLost()
        {
            playerLost(false);
        }
        #endregion
    }
}
