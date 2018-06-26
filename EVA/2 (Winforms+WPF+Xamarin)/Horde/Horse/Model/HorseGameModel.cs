using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Horse.Model
{
    public enum GameDifficulty { easy, medium, hard }

    public class HorseGameModel
    {
        private int[,] table;
        private int figureX;
        private int figureY;
        private int score;
        private int fieldsDone;
        private int gameStepCount;
        private int maxsize;
        private int gameTime;
        public Int32[,] Table { get { return table; } }
        public Int32 FigureX { get { return figureX; } }
        public Int32 FigureY { get { return figureY; } }
        public Int32 FieldsDone { get { return fieldsDone; } }
        public Int32 GameStepCount { get { return gameStepCount; } }
        public int Score { get { return score; } }
        public int Size { get { return table.GetLength(0); } }
        public Boolean IsGameOver { get { return ( fieldsDone == maxsize); } }
        public event EventHandler<HorseEventArgs> StepReload;
        public event EventHandler<HorseEventArgs> GameOver;
        public event EventHandler<HorseEventArgs> StepBack;
        public event EventHandler<HorseEventArgs> GameAdvanced;

        Random random;

        public HorseGameModel()
        {
            table = new int[3, 3];
        }

        public void NewGame(int size)
        {
            fieldsDone = 1;
            gameStepCount = 0;
            figureX = 0;
            figureY = 0;
            score = 0;
            table = new int[size, size];
            maxsize = size * size;
            gameTime = 0;

            random = new Random();
            InitTable();
        }

        public bool Step(Int32 x, Int32 y)
        {
            if (!CheckStep(x,y))
                return false;

            if(table[x,y] == 0)
            {
                score += 2;
                fieldsDone++;
            }
            else
            {
                score -= 1;
            }
            table[figureX, figureY] = 1;
            gameStepCount++;

            OnStepReload(x, y, figureX, figureY);

            if (gameStepCount % Size == 0)
            {
                int randX;
                int randY;
                do
                {
                    randX = random.Next(0, Size - 1);
                    randY = random.Next(0, Size - 1);
                }
                while (table[randX,randY] != 1 || randX == figureX && randY == figureY);
                table[randX, randY] = 0;
                if(fieldsDone > 0)
                {
                    fieldsDone--;
                }
                OnStepBack(randX, randY);
            }

            figureX = x;
            figureY = y;

            if (fieldsDone == maxsize) // ha vége a játéknak, jelezzük, hogy győztünk
            {
                OnGameOver();
            }
            return true;
        }

        private Boolean CheckStep(int x, int y)
        {
            if(Math.Abs(x - figureX) == 1 && Math.Abs(y-figureY) == 2 || 
                Math.Abs(x - figureX) == 2 && Math.Abs(y - figureY) == 1)
            {
                return true;
            }
            return false;
        }

        public void AdvanceTime()
        {
            gameTime++;
            OnGameAdvanced();
        }

        private void InitTable()
        {
            for(int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size; j++)
                {
                    table[i, j] = 0;
                }
            }
        }

        private void OnStepReload(int x, int y, int lx, int ly)
        {
            StepReload?.Invoke(this, new HorseEventArgs(x, y, lx, ly));
        }

        private void OnStepBack(int x, int y)
        {
            StepBack?.Invoke(this, new HorseEventArgs(x, y));
        }

        private void OnGameOver()
        {
            GameOver?.Invoke(this, new HorseEventArgs(score));
        }

        private void OnGameAdvanced()
        {
            GameAdvanced?.Invoke(this, new HorseEventArgs(gameTime, true));
        }
    }
}
