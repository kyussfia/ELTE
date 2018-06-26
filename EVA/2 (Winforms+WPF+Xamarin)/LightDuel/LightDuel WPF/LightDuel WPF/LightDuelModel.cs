using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace LightDuel_WinForms.Model  
{
    #region PlayerMoveEventArgs
    public class PlayerMoveEventArgs : EventArgs
    {
        public int BlueX { get; private set; }

        public int BlueY { get; private set; }

        public int RedX { get; private set; }

        public int RedY { get; private set; }

        public PlayerMoveEventArgs(int bx, int by, int rx, int ry)
        {
            BlueX = bx;
            BlueY = by;
            RedX = rx;
            RedY = ry;
        }
    }
    #endregion

    #region GameOverEventArgs

    public class GameOverEventArgs : EventArgs
    {
        public bool BlueLost { get; private set; }

        public bool RedLost { get; private set; }

        public GameOverEventArgs(bool bl, bool rl)
        {
            BlueLost = bl;
            RedLost = rl;
        }
    }
    #endregion

    public class LightDuelModel : Object
    {
        #region Helper Types : Players, Player

        public enum Players { No, Blue, Red };

        public class Player
        {
            public int col;
            public int row;
            public int dir;
            public Players type;

            public Player(int c, int r, int d, bool isBlue)
            {
                col = c;
                row = r;
                dir = d;
                type = isBlue ? Players.Blue : Players.Red;
            }

            public void left()
            {
                switch(dir)
                {
                    case 1:
                        dir = 4;
                        break;
                    case 2:
                        dir = 1;
                        break;
                    case 3:
                        dir = 2;
                        break;
                    case 4:
                        dir = 3;
                        break;
                }
            }

            public void right()
            {
                switch (dir)
                {
                    case 1:
                        dir = 2;
                        break;
                    case 2:
                        dir = 3;
                        break;
                    case 3:
                        dir = 4;
                        break;
                    case 4:
                        dir = 1;
                        break;
                }
            }
        }
        #endregion

        #region Private Fields;

        private int gameSize;        

        public List<List<Players>> fields;

        #endregion

        #region Events & Handlers

        public event EventHandler<PlayerMoveEventArgs> ticked;

        private bool isPaused; // for testing

        public event EventHandler<GameOverEventArgs> gameOver;

        private void OnTick()
        {
            if (movePlayers())
            {
                ticked?.Invoke(this, new PlayerMoveEventArgs(Blue.col, Blue.row, Red.col, Red.row));
            }
        }

        private void OnGameOver()
        {
            gameOver?.Invoke(this, new GameOverEventArgs(blueLost, redLost));
        }

        #endregion

        #region Interface methods

        public int speed = 500; //1000 millisechez képest mennyi idő legyen 1 lépés?

        public int GameSize { get { return gameSize; } }

        public Player Blue;

        public Player Red;

        public bool blueLost;

        public bool redLost;

        public LightDuelModel()
        {
            isPaused = true;
        }

        public bool isBluePosition(int col, int row)
        {
            return fields[col][row] == Players.Blue;
        }

        public bool isRedPosition(int col, int row)
        {
            return fields[col][row] == Players.Red;
        }

        public void newGame(int gameSize)
        {
            isPaused = false; //for test only

            initGame(gameSize);

            initTable();
        }

        //for testing only
        public void handlePausing(bool on)
        {
            if (on)
            {
                isPaused = false;
            }
            else
            {
                isPaused = true;
            }
        }

        //for testing only
        public bool isTimerWorks()
        {
            return !isPaused;
        }
        
        public void performTick()
        {
            OnTick();
        }

        #endregion

        #region InGame Methods

        private bool movePlayers()
        {
            blueLost = !this.movePlayer(Blue);
            redLost = !this.movePlayer(Red);

            if (blueLost || redLost)
            {
                isPaused = true;
                OnGameOver();
                return false;
            }
            return true;
        }

        private bool ableToMoveHere(int col, int row)
        {
            return col < gameSize && row < gameSize && col >= 0 && row >= 0;
        }

        private bool controlledByPlayer(int col, int row)
        {
            return fields[col][row] != Players.No;
        }

        private bool movePlayer(Player player)
        {
            bool movable = false;
            if (player.dir == 1 && ableToMoveHere(player.col, player.row - 1) && !controlledByPlayer(player.col, player.row - 1)) //fel
            {
                player.row = player.row - 1;
                movable = true;
            }
            else if (player.dir == 2 && ableToMoveHere(player.col + 1, player.row) && !controlledByPlayer(player.col + 1, player.row)) //jobb
            {
                player.col = player.col + 1;
                movable = true;
            }
            else if (player.dir == 3 && ableToMoveHere(player.col, player.row + 1) && !controlledByPlayer(player.col, player.row + 1)) //le
            {
                player.row = player.row + 1;
                movable = true;
            }
            else if (player.dir == 4 && ableToMoveHere(player.col - 1, player.row) && !controlledByPlayer(player.col - 1, player.row)) // bal
            {
                player.col = player.col - 1;
                movable = true;
            }

            if (movable)
            {
                fields[player.col][player.row] = player.type;
                return true;
            } else   
            {
                return false;
            } 
        }

        private void initGame(int gameSize)
        {
            this.fields = new List<List<Players>>();
            this.gameSize = gameSize;

            blueLost = false;
            redLost = false;

            Blue = new Player(0, (gameSize / 2) - 1, 2, true);
            Red = new Player(gameSize - 1, (gameSize / 2), 4, false);
        }

        private void initTable()
        {
            for (int i = 0; i < gameSize; i++)
            {
                fields.Add(new List<Players>());
                for (int j = 0; j < gameSize; j++)
                {
                    if (Blue.col == i && Blue.row == j)
                    {
                        fields[i].Add(Players.Blue);
                    }
                    else if (Red.col == i && Red.row == j)
                    {
                        fields[i].Add(Players.Red);
                    }
                    else
                    {
                        fields[i].Add(Players.No);
                    }
                }
            }
        }

        #endregion
    }
}
