using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace LightDuel_WPF
{
    #region DelegateCommand
    public class DelegateCommand : ICommand
    {
        private readonly Action<Object> _execute; // a tevékenységet végrehajtó lambda-kifejezés
        private readonly Func<Object, Boolean> _canExecute; // a tevékenység feltételét ellenőző lambda-kifejezés

        /// <summary>
        /// Parancs létrehozása.
        /// </summary>
        /// <param name="execute">Végrehajtandó tevékenység.</param>
        public DelegateCommand(Action<Object> execute) : this(null, execute) { }

        /// <summary>
        /// Parancs létrehozása.
        /// </summary>
        /// <param name="canExecute">Végrehajthatóság feltétele.</param>
        /// <param name="execute">Végrehajtandó tevékenység.</param>
        public DelegateCommand(Func<Object, Boolean> canExecute, Action<Object> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Végrehajthatóság változásának eseménye.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Végrehajthatóság ellenőrzése
        /// </summary>
        /// <param name="parameter">A tevékenység paramétere.</param>
        /// <returns>Igaz, ha a tevékenység végrehajtható.</returns>
        public Boolean CanExecute(Object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        /// <summary>
        /// Tevékenység végrehajtása.
        /// </summary>
        /// <param name="parameter">A tevékenység paramétere.</param>
        public void Execute(Object parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException("Command execution is disabled.");
            }
            _execute(parameter);
        }

        /// <summary>
        /// Végrehajthatóság változásának eseménykiváltása.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }

    #endregion

    #region ViewModelBase

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Nézetmodell ősosztály példányosítása.
        /// </summary>
        protected ViewModelBase() { }

        /// <summary>
        /// Tulajdonság változásának eseménye.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Tulajdonság változása ellenőrzéssel.
        /// </summary>
        /// <param name="propertyName">Tulajdonság neve.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    #endregion

    #region GameViewModel

    class GameViewModel : ViewModelBase
    {
        #region Field
        public class Field : ViewModelBase
        {
            private Color color;

            public Color Color
            {
                get { return color; }
                set
                {
                    if (color != value)
                    {
                        color = value;
                        OnPropertyChanged();
                    }
                }
            }
            /// <summary>
            /// Vízszintes koordináta lekérdezése, vagy beállítása.
            /// </summary>
            public Int32 X { get; set; }

            /// <summary>
            /// Függőleges koordináta lekérdezése, vagy beállítása.
            /// </summary>
            public Int32 Y { get; set; }

            /// <summary>
            /// Sorszám lekérdezése.
            /// </summary>
            public Int32 Number { get; set; }
        }
        #endregion

        private LightDuel_WinForms.Model.LightDuelModel model;
        private int clockCounter;
        private int size;
        private int periodCounter;
        private int TimePeriod;

        private bool disableKeys;
        private bool isPaused;
        private bool inGame;

        private System.Windows.Threading.DispatcherTimer baseTimer;

        public string PauseText { get { return isPaused ? "Start" : "Pause"; } }
        public ObservableCollection<Field> Fields { get; set; }
        public DelegateCommand LittleCommand { get; private set; }
        public DelegateCommand MidCommand { get; private set; }
        public DelegateCommand LargeCommand { get; private set; }
        public DelegateCommand PauseCommand { get; private set; }

        private const Key P1Left = Key.A; //key A
        private const Key P1Right = Key.D; //key D
        private const Key P2Left = Key.Left; //key left arrow
        private const Key P2Right = Key.Right; // key right arrow

        public String Time { get { return TimeSpan.FromSeconds(clockCounter).ToString("g"); } }

        public void handleKeyUp(Key key)
        {
            if (!disableKeys)
            {
                if (key == P1Left)
                {
                    model.Blue.left();
                }

                if (key == P1Right)
                {
                    model.Blue.right();
                }

                if (key == P2Left)
                {
                    model.Red.left();
                }

                if (key == P2Right)
                {
                    model.Red.right();
                }
            }
        }

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

        public GameViewModel(LightDuel_WinForms.Model.LightDuelModel model)
        {
            this.model = model;
            periodCounter = 0;
            TimePeriod = (1000 / model.speed) - 1;
            disableKeys = true;
            isPaused = false;
            inGame = false;

            Fields = new ObservableCollection<Field>();

            baseTimer = new System.Windows.Threading.DispatcherTimer();
            baseTimer.Interval = new TimeSpan(0, 0, 0, 0, model.speed);
            baseTimer.Tick += BaseTimer_Tick;

            LittleCommand = new DelegateCommand(param => { startGame(12); });
            MidCommand = new DelegateCommand(param => { startGame(24); });
            LargeCommand = new DelegateCommand(param => { startGame(36); });

            PauseCommand = new DelegateCommand(param => { pause(); });
        }

        private void BaseTimer_Tick(object sender, EventArgs e)
        {
            model.performTick();
        }

        public void handleTick(int bX, int bY, int rX, int rY)
        {
            if (periodCounter == TimePeriod)
            {
                clockCounter++;
                OnPropertyChanged("Time");
                periodCounter = 0;
            }
            else
            {
                periodCounter++;
            }
            Fields[bX * Size + bY].Color = GetBrushFor(bX, bY);
            Fields[rX * Size + rY].Color = GetBrushFor(rX, rY);
            refreshTable();
        }

        private void startGame(int n)
        {
            resetGame();
            Size = n;
            Fields.Clear();
            model.newGame(n);
            inGame = true;

            for (Int32 i = 0; i < n; i++) 
            {
                for (Int32 j = 0; j < n; j++)
                {
                    Fields.Add(new Field
                    {
                        Color = Colors.Transparent,
                        X = i,
                        Y = j,
                        Number = i * n + j
                    });
                }
            }
            refreshTable();
            baseTimer.Start();
        }

        private void pause()
        {
            if (inGame)
            {
                handlePausing();
                isPaused = isPaused ? false : true;
                disableKeys = isPaused ? true : false;
                OnPropertyChanged("PauseText");
            }
        }

        public void handlePausing()
        {
            if (isPaused)
            {
                baseTimer.Start();
            }
            else
            {
                baseTimer.Stop();
            }
        }

        private Color GetBrushFor(int col, int row)
        {
            if (model.isBluePosition(col, row))
            {
                return Colors.Blue;
            }
            else if (model.isRedPosition(col, row))
            {
                return Colors.Red;
            }
            else
            {
                return Colors.Transparent;
            }
        }

        public void refreshTable()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Fields[i * Size + j].Color = GetBrushFor(j, i);
                    OnPropertyChanged("Fields");
                }
            }
        }

        public void clearGame()
        {
            disableKeys = false;
            isPaused = false;
            inGame = false;
            baseTimer.Stop();
        }

        private void resetGame()
        {
            clearGame();
            periodCounter = 0;
            clockCounter = 0;
            OnPropertyChanged("PauseText");
            OnPropertyChanged("Time");
        }
    }
    #endregion
}
