using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;    
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Android.Views;

namespace LightDuel
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
                PropertyChanged(this, new   PropertyChangedEventArgs(propertyName));
            }
        }
    }

    #endregion

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

        #region private fields 
        private LightDuel.Model model;
        private int clockCounter;
        private int size;
        private int periodCounter;
        private int TimePeriod;

        private bool disableKeys;
        private bool isPaused;
        private bool inGame;
        public bool timer;

        #endregion

        #region Props

        public string PauseText { get { return isPaused ? "Start" : "Pause"; } }
        public ObservableCollection<Field> Fields { get; set; }
        public DelegateCommand LittleCommand { get; private set; }
        public DelegateCommand MidCommand { get; private set; }
        public DelegateCommand LargeCommand { get; private set; }
        public DelegateCommand PauseCommand { get; private set; }
        public DelegateCommand MenuCommand { get; private set; }

        public DelegateCommand P1LCommand { get; private set; }
        public DelegateCommand P1RCommand { get; private set; }
        public DelegateCommand P2LCommand { get; private set; }
        public DelegateCommand P2RCommand { get; private set; }

        public event EventHandler<EventArgs> menuOpened;

        public String Time { get { return TimeSpan.FromSeconds(clockCounter).ToString("g"); } }

        public int ButtonHeight
        {
            get
            {
                if (size == 12)
                    return 27;
                if (size == 24)
                    return 15;
                if (size == 36)
                    return 9;
                return 0;
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

        #endregion

        #region Class methods

        public GameViewModel(LightDuel.Model model)
        {
            this.model = model;
            periodCounter = 0;
            TimePeriod = (1000 / model.speed) - 1;
            disableKeys = true;
            isPaused = false;
            inGame = false;
            timer = false;

            Fields = new ObservableCollection<Field>();

            LittleCommand = new DelegateCommand(param => { startGame(12); });
            MidCommand = new DelegateCommand(param => { startGame(24); });
            LargeCommand = new DelegateCommand(param => { startGame(36); });

            PauseCommand = new DelegateCommand(param => { pause(false); });
            MenuCommand = new DelegateCommand(param => { OnSleep(); menuOpened?.Invoke(this, EventArgs.Empty); });

            P1LCommand = new DelegateCommand(param => { if (!disableKeys) { model.Blue.left(); } });
            P1RCommand = new DelegateCommand(param => { if (!disableKeys) { model.Blue.right(); } });
            P2LCommand = new DelegateCommand(param => { if (!disableKeys) { model.Red.left(); } });
            P2RCommand = new DelegateCommand(param => { if (!disableKeys) { model.Red.right(); } });
        }

        public void OnSleep()
        {
            if (!isPaused) { pause(true); }
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
            Size = n;
            resetGame();
            Fields.Clear();
            model.newGame(n);
            inGame = true;

            for (Int32 i = 0; i < n; i++)
            {
                for (Int32 j = 0; j < n; j++)
                {
                    Fields.Add(new Field
                    {
                        Color = Color.Transparent,  
                        X = i,
                        Y = j,
                        Number = i * n + j
                    });
                }
            }
            refreshTable();

            timer = true;
            Device.StartTimer(new TimeSpan(0, 0, 0, 0, model.speed), () => { if (timer) { model.performTick(); return true; } return false; }); // elindítjuk az időzítőt
        }

        private void pause(bool on)
        {
            if (inGame || on)
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
                timer = true;
                Device.StartTimer(new TimeSpan(0, 0, 0, 0, model.speed), () => { if (timer) { model.performTick(); return true; } return false; }); // elindítjuk az időzítőt
            }
            else
            {
                timer = false;
            }
        }

        private Color GetBrushFor(int col, int row)
        {
            if (model.isBluePosition(col, row))
            {
                return Color.Blue;
            }
            else if (model.isRedPosition(col, row))
            {
                return Color.Red;
            }
            else
            {
                return Color.Transparent;
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
            timer = false;
        }

        private void resetGame()
        {
            clearGame();
            periodCounter = 0;
            clockCounter = 0;
            OnPropertyChanged("PauseText");
            OnPropertyChanged("Time");
            OnPropertyChanged("ButtonHeight");
        }

        #endregion
    }
}
