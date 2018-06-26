using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TableBase.Model;
using TableBase.ViewModel;

namespace TableBase
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        GameModel model;
        GameViewModel viewModel;
        MainWindow view;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            // modell létrehozása
            model = new GameModel();
            model.GameOver += new EventHandler<GameEventArgs>(Model_GameOver);
      
            // nézemodell létrehozása
            viewModel = new GameViewModel(model);

            // nézet létrehozása
            view = new MainWindow();
            view.DataContext = viewModel;
            //view.Closing += new System.ComponentModel.CancelEventHandler(View_Closing); // eseménykezelés a bezáráshoz
            view.Show();
        }

        private void Model_GameOver(object sender, GameEventArgs e)
        {
            viewModel.Timer.Stop();
            MessageBox.Show("Fuck yeah!",
                            "Rubik tábla",
                            MessageBoxButton.OK,
                            MessageBoxImage.Asterisk);

            viewModel.SetUpGame(viewModel.Size);
        }
    }
}
