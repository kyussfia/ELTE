using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace LightDuel
{
    public class App : Application
    {
        #region Fields

        private Model model;
        private GameViewModel viewModel;
        private GamePage gamePage;

        #endregion

        #region Application methods

        public App()
        {
            model = new Model();
            model.ticked += new EventHandler<PlayerMoveEventArgs>(clockTicked);
            model.gameOver += new EventHandler<GameOverEventArgs>(gameOver);

            viewModel = new GameViewModel(model);
            viewModel.menuOpened += new EventHandler<EventArgs>(SizeChooser);

            gamePage = new GamePage();
            gamePage.BindingContext = viewModel;

            MainPage = gamePage;
        }

        protected override void OnStart()
        {
            SizeChooser(this, EventArgs.Empty);
        }

        private async void SizeChooser(object sender, EventArgs e)
        {
            var answer = await MainPage.DisplayActionSheet("Válassz méretet!", null, null, "12 x 12", "24 x 24", "36 x 36");

            switch ((string)answer)
            {
                case "12 x 12":
                    viewModel.LittleCommand.Execute(null);
                    break;
                case "24 x 24":
                    viewModel.MidCommand.Execute(null);
                    break;
                case "36 x 36":
                    viewModel.LargeCommand.Execute(null);
                    break;
            }
        }

        private void clockTicked(object sender, PlayerMoveEventArgs e)
        {
            viewModel.handleTick(e.BlueX, e.BlueY, e.RedX, e.RedY);
        }

        private void gameOver(object sender, GameOverEventArgs e)
        {
            viewModel.clearGame();
            if (e.BlueLost && e.RedLost)
            {
                tied();
            }
            else if (e.BlueLost)
            {
                blueLost();
            }
            else if (e.RedLost)
            {
                redLost();
            }
        }
        private async void tied()
        {
            await MainPage.DisplayAlert("Light-Duel", "Döntetlen" + Environment.NewLine +
                                " Elért idő:  " + viewModel.Time,
                                            "OK");
        }

        private async void playerLost(bool isBlue = true)
        {
            await MainPage.DisplayAlert("Light-Duel - Győzelem", "A győztes: " + Environment.NewLine +
                                (isBlue ? "Piros játékos" : "Kék játékos") +
                                " Elért idő:  " + viewModel.Time,
                                            "OK");
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

        protected override void OnSleep()
        {
            try
            {
                viewModel.OnSleep();
            }
            catch { }
        }

        protected override void OnResume()
        {
            //game already paused, ready to go
        }
    }
}
