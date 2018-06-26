using System;

namespace TableBase.Model
{
    public class GameModel
    {
        public int[,] table;
        public int size;
        public bool started = false;
        public int gameStepCount;

        public event EventHandler<GameEventArgs> GameOver;

        public GameModel()
        {
            gameStepCount = 0;
            started = false;
        }

        public void NewGame(int n)
        {
            started = false;
            table = new int[n, n];
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    table[i, j] = i;
                }
            }
            size = n;
            Mix(size);
            started = true;
        }

        public void Step(int x, int y, char direction)
        {
            int remember;
            switch(direction)
            {
                case 'u':
                    remember = table[0, y];
                    for(int i = 0; i < size-1; i++)
                    {
                        table[i, y] = table[i + 1, y];
                    }
                    table[size - 1, y] = remember;
                    break;

                case 'd':
                    remember = table[size-1, y];
                    for (int i = size - 1; i > 0; i--)
                    {
                        table[i, y] = table[i - 1, y];
                    }
                    table[0, y] = remember;
                    break;

                case 'l':
                    remember = table[x, 0];
                    for (int i = 0; i < size - 1; i++)
                    {
                        table[x, i] = table[x, i + 1];
                    }
                    table[x, size - 1] = remember;
                    break;

                case 'r':
                    remember = table[x, size - 1];
                    for (int i = size - 1; i > 0; i--)
                    {
                        table[x, i] = table[x, i - 1];
                    }
                    table[x, 0] = remember;
                    break;
            }

            if((IsGameOverRow() || IsGameOverColumn()) && started)
            {
                OnGameOver();
            }

            if (started)
            {
                gameStepCount++;
                if (gameStepCount == size + 1)
                {
                    gameStepCount = 0;
                }
            }
        }

        public void Mix(int n)
        {
            Random random = new Random();
            for(int i = 0; i < n * n * n; i++)
            {
                int dir = random.Next(1, 4);
                int row = random.Next(0, n - 1);
                int column = random.Next(0, n - 1);
                switch(dir)
                {
                    case 1:
                        Step(row, column, 'u');
                        break;
                    case 2:
                        Step(row, column, 'd');
                        break;
                    case 3:
                        Step(row, column, 'l');
                        break;
                    case 4:
                        Step(row, column, 'r');
                        break;
                }
            }
        }

        public Boolean IsGameOverRow()
        {
            for(int i = 0; i < size; i++)
            {
                for(int j = 1; j < size; j++)
                {
                    if(table[i,j] != table[i,j-1])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Boolean IsGameOverColumn()
        {
            for (int i = 1; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (table[i, j] != table[i - 1, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void OnGameOver()
        {
            if (GameOver != null)
                GameOver(this, new GameEventArgs());
        }
    }
}
