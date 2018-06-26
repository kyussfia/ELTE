using System;
using System.Threading.Tasks;
using ELTE.Sudoku.Model;
using ELTE.Sudoku.Persistence;
using ELTE.Sudoku.View;
using ELTE.Sudoku.ViewModel;
using Xamarin.Forms;

namespace ELTE.Sudoku
{
    public class App : Application
    {
        #region Fields

        private ISudokuDataAccess _sudokuDataAccess;
        private SudokuGameModel _sudokuGameModel;
        private SudokuViewModel _sudokuViewModel;
        private GamePage _gamePage;
        private SettingsPage _settingsPage;

        private IStore _store;
        private StoredGameBrowserModel _storedGameBrowserModel;
        private StoredGameBrowserViewModel _storedGameBrowserViewModel;
        private LoadGamePage _loadGamePage;
        private SaveGamePage _saveGamePage;

        private Boolean _advanceTimer;
        private NavigationPage _mainPage;

        #endregion

        #region Application methods

        public App()
        {
            // játék összeállítása
            _sudokuDataAccess = DependencyService.Get<ISudokuDataAccess>(); // az interfész megvalósítását automatikusan megkeresi a rendszer
            
            _sudokuGameModel = new SudokuGameModel(_sudokuDataAccess);
            _sudokuGameModel.GameOver += new EventHandler<SudokuEventArgs>(SudokuGameModel_GameOver);

            _sudokuViewModel = new SudokuViewModel(_sudokuGameModel);
            _sudokuViewModel.NewGame += new EventHandler(SudokuViewModel_NewGame);
            _sudokuViewModel.LoadGame += new EventHandler(SudokuViewModel_LoadGame);
            _sudokuViewModel.SaveGame += new EventHandler(SudokuViewModel_SaveGame);
            _sudokuViewModel.ExitGame += new EventHandler(SudokuViewModel_ExitGame);

            _gamePage = new GamePage();
            _gamePage.BindingContext = _sudokuViewModel;

            _settingsPage = new SettingsPage();
            _settingsPage.BindingContext = _sudokuViewModel;

            // a játékmentések kezelésének összeállítása
            _store = DependencyService.Get<IStore>(); // a perzisztencia betöltése az adott platformon
            _storedGameBrowserModel = new StoredGameBrowserModel(_store);
            _storedGameBrowserViewModel = new StoredGameBrowserViewModel(_storedGameBrowserModel);
            _storedGameBrowserViewModel.GameLoading += new EventHandler<StoredGameEventArgs>(StoredGameBrowserViewModel_GameLoading);
            _storedGameBrowserViewModel.GameSaving += new EventHandler<StoredGameEventArgs>(StoredGameBrowserViewModel_GameSaving);

            _loadGamePage = new LoadGamePage();
            _loadGamePage.BindingContext = _storedGameBrowserViewModel;

            _saveGamePage = new SaveGamePage();
            _saveGamePage.BindingContext = _storedGameBrowserViewModel;

            // nézet beállítása
            _mainPage = new NavigationPage(_gamePage); // egy navigációs lapot használunk fel a három nézet kezelésére

            MainPage = _mainPage;
        }

        protected override void OnStart()
        {
            _sudokuGameModel.NewGame();
            _sudokuViewModel.RefreshTable();
            _advanceTimer = true; // egy logikai értékkel szabályozzuk az időzítőt
            Device.StartTimer(TimeSpan.FromSeconds(1), () => { _sudokuGameModel.AdvanceTime(); return _advanceTimer; }); // elindítjuk az időzítőt
        }

        protected override void OnSleep()
        {
            _advanceTimer = false;

            // elmentjük a jelenleg folyó játékot
            try
            {
                Task.Run(async () => await _sudokuGameModel.SaveGame("SuspendedGame"));
            }
            catch { }
        }

        protected override void OnResume()
        {
            // betöltjük a felfüggesztett játékot, amennyiben van
            try
            {
                Task.Run(async () =>
                {
                    await _sudokuGameModel.LoadGame("SuspendedGame");
                    _sudokuViewModel.RefreshTable();

                    // csak akkor indul az időzítő, ha sikerült betölteni a játékot
                    _advanceTimer = true;
                    Device.StartTimer(TimeSpan.FromSeconds(1), () => { _sudokuGameModel.AdvanceTime(); return _advanceTimer; });
                });
            }
            catch { }

        }

        #endregion

        #region ViewModel event handlers

        /// <summary>
        /// Új játék indításának eseménykezelője.
        /// </summary>
        private void SudokuViewModel_NewGame(object sender, EventArgs e)
        {
            _sudokuGameModel.NewGame();

            if (!_advanceTimer)
            {
                // ha nem fut az időzítő, akkor elindítjuk
                _advanceTimer = true;
                Device.StartTimer(TimeSpan.FromSeconds(1), () => { _sudokuGameModel.AdvanceTime(); return _advanceTimer; });
            }
        }

        /// <summary>
        /// Játék betöltésének eseménykezelője.
        /// </summary>
        private async void SudokuViewModel_LoadGame(object sender, System.EventArgs e)
        {
            await _storedGameBrowserModel.UpdateAsync(); // frissítjük a tárolt játékok listáját
            await _mainPage.PushAsync(_loadGamePage); // átnavigálunk a lapra
        }

        /// <summary>
        /// Játék mentésének eseménykezelője.
        /// </summary>
        private async void SudokuViewModel_SaveGame(object sender, EventArgs e)
        {
            await _storedGameBrowserModel.UpdateAsync(); // frissítjük a tárolt játékok listáját
            await _mainPage.PushAsync(_saveGamePage); // átnavigálunk a lapra
        }

        private async void SudokuViewModel_ExitGame(object sender, EventArgs e)
        {
            await _mainPage.PushAsync(_settingsPage); // átnavigálunk a beállítások lapra
        }


        /// <summary>
        /// Betöltés végrehajtásának eseménykezelője.
        /// </summary>
        private async void StoredGameBrowserViewModel_GameLoading(object sender, StoredGameEventArgs e)
        {
            await _mainPage.PopAsync(); // visszanavigálunk

            // betöltjük az elmentett játékot, amennyiben van
            try
            {
                await _sudokuGameModel.LoadGame(e.Name);
                _sudokuViewModel.RefreshTable();

                // csak akkor indul az időzítő, ha sikerült betölteni a játékot
                _advanceTimer = true;
                Device.StartTimer(TimeSpan.FromSeconds(1), () => { _sudokuGameModel.AdvanceTime(); return _advanceTimer; });
            }
            catch
            {
                await MainPage.DisplayAlert("Sudoku játék", "Sikertelen betöltés.", "OK");
            }
        }

        /// <summary>
        /// Mentés végrehajtásának eseménykezelője.
        /// </summary>
        private async void StoredGameBrowserViewModel_GameSaving(object sender, StoredGameEventArgs e)
        {
            await _mainPage.PopAsync(); // visszanavigálunk
            _advanceTimer = false;

            try
            {
                // elmentjük a játékot
                await _sudokuGameModel.SaveGame(e.Name);
            }
            catch { }

            await MainPage.DisplayAlert("Sudoku játék", "Sikeres mentés.", "OK");
        }

        #endregion

        #region Model event handlers

        /// <summary>
        /// Játék végének eseménykezelője.
        /// </summary>
        private async void SudokuGameModel_GameOver(object sender, SudokuEventArgs e)
        {
            _advanceTimer = false;

            if (e.IsWon) // győzelemtől függő üzenet megjelenítése
            {
                await MainPage.DisplayAlert("Sudoku játék", "Gratulálok, győztél!" + Environment.NewLine +
                                            "Összesen " + e.GameStepCount + " lépést tettél meg és " +
                                            TimeSpan.FromSeconds(e.GameTime).ToString("g") + " ideig játszottál.",
                                            "OK");
            }
            else
            {
                await MainPage.DisplayAlert("Sudoku játék", "Sajnálom, vesztettél, lejárt az idő!", "OK");
            }
        }

        #endregion
    }
}
