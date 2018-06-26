using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TableBase.Model;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace TableBase.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        private GameModel model;
        private int size = 0;
        private DispatcherTimer timer;
        private int timerCount;
        char dir = 'u';
        char[] directions = { 'u', 'd', 'l', 'r' };
        public ObservableCollection<Field> Fields { get; set; }

        public DelegateCommand Lvl1Command { get; private set; }
        public DelegateCommand Lvl2Command { get; private set; }
        public DelegateCommand Lvl3Command { get; private set; }
        public DelegateCommand SetDirectionCommand { get; private set; }

        public String Time { get { return TimeSpan.FromSeconds(timerCount).ToString("g"); } }
        public DispatcherTimer Timer { get { return timer; } }

        public int Size
        {
            get { return size; }
            set
            {
                if (size != value)
                {
                    size = value;
                    OnPropertyChanged();
                }
            }
        }

        public GameViewModel(GameModel model)
        {
            this.model = model;

            Lvl1Command = new DelegateCommand(param => { SetUpGame(3); Size = 3; });
            Lvl2Command = new DelegateCommand(param => { SetUpGame(4); Size = 4; });
            Lvl3Command = new DelegateCommand(param => { SetUpGame(6); Size = 6; });

            SetDirectionCommand = new DelegateCommand(param => { SetDirection(Convert.ToChar(param)); });

            model.GameOver += new EventHandler<GameEventArgs>(Model_GameOver);
        }

        public void SetUpGame(int n)
        {
            timerCount = 0;
            Size = n;
            Fields = new ObservableCollection<Field>();
            for (Int32 i = 0; i < n; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < n; j++)
                {
                    Fields.Add(new Field
                    {
                        Color = i,
                        X = i,
                        Y = j,
                        Number = i * n + j, // a gomb sorszáma, amelyet felhasználunk az azonosításhoz
                        //StepCommand = new DelegateCommand(param => StepGame(Convert.ToInt32(param)))
                        // ha egy mezőre léptek, akkor jelezzük a léptetést, változtatjuk a lépésszámot
                    });
                }
            }
            model.NewGame(n);
            RefreshTable();

            if (timer == null)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(Timer_Tick);
                timer.Start();
            }
            else
            {
                timer.Start();
            }
        }

        public void SetDirection(char c)
        {
            dir = c;
        }

        public void StepGame(int index)
        {
            Field field = Fields[index];
            model.Step(field.X, field.Y, dir);
            RefreshTable();
            if(model.gameStepCount == Size)
            {     
                Random random = new Random();
                int dir = random.Next(0, 3);
                int row = random.Next(0, size - 1);
                int column = random.Next(0, size - 1);
                model.Step(row, column, directions[dir]);
                RefreshTable();
            }
        }

        public void RefreshTable()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Fields[i * Size + j].Color = model.table[i, j];
                    OnPropertyChanged("Fields");
                }
            }
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            timerCount++;
            OnPropertyChanged("Time");
        }

        public void Model_GameOver(object sender, GameEventArgs e)
        {
            RefreshTable();
        }
    }
}
