using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LightDuel_WinForms.Model;
using System.Windows.Input;

namespace LightDuel_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        LightDuelModel model;
        GameViewModel viewModel;
        MainWindow view;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            // modell létrehozása
            model = new LightDuelModel();
  
            model.ticked += new EventHandler<PlayerMoveEventArgs>(clockTicked);
            model.gameOver += new EventHandler<GameOverEventArgs>(gameOver);

            // nézemodell létrehozása
            viewModel = new GameViewModel(model);

            // nézet létrehozása
            view = new MainWindow();
            view.DataContext = viewModel;
            view.KeyUp += new KeyEventHandler(MainWindow_KeyUp);
            view.Show();
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            viewModel.handleKeyUp(e.Key);
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
                this.tied();
            } else if (e.BlueLost)
            {
                this.blueLost();
            } else if (e.RedLost)
            {
                this.redLost();
            }
        }

        private void tied()
        {
            MessageBox.Show("Döntetlen" + Environment.NewLine +
                                " Elért idő:  " + viewModel.Time,
                                "Light-Duel",
                                MessageBoxButton.OK,
                                MessageBoxImage.Asterisk);
        }

        private void playerLost(bool isBlue = true)
        {
            MessageBox.Show("A győztes: " + Environment.NewLine +
                                (isBlue ? "Piros játékos" : "Kék játékos") +
                                " Elért idő:  " + viewModel.Time,
                                "Light-Duel - Győzele   m",
                                MessageBoxButton.OK,
                                MessageBoxImage.Asterisk);
        }

        private void blueLost()
        {
            playerLost(true);
        }

        private void redLost()
        {
            playerLost(false);
        }
    }
}