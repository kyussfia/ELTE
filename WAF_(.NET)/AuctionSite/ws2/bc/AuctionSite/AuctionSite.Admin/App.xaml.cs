using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AuctionSite.Admin.Model;
using AuctionSite.Admin.Persistence;
using AuctionSite.Admin.View;
using AuctionSite.Admin.ViewModel;
using System.Diagnostics;

namespace AuctionSite.Admin
{
    public partial class App : Application
    {
        private IAuctionSiteModel _model;
        private LoginViewModel _loginViewModel;
        private LoginWindow _loginView;
        private MainViewModel _mainViewModel;
        private MainWindow _mainView;
        private ItemEditor _editorView;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
            Exit += new ExitEventHandler(App_Exit);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _model = new AuctionSiteModel(new AuctionSiteServicePersistence("http://localhost:55923/")); // megadjuk a szolgáltatás címét

            _loginViewModel = new LoginViewModel(_model);
            _loginViewModel.ExitApplication += new EventHandler(ViewModel_ExitApplication);
            _loginViewModel.LoginSuccess += new EventHandler(ViewModel_LoginSuccess);
            _loginViewModel.LoginFailed += new EventHandler(ViewModel_LoginFailed);

            _loginView = new LoginWindow();
            _loginView.DataContext = _loginViewModel;
            _loginView.Show();
        }

        public async void App_Exit(object sender, ExitEventArgs e)
        {
            if (_model.IsUserLoggedIn) // amennyiben be vagyunk jelentkezve, kijelentkezünk
            {
                await _model.LogoutAsync();
            }
        }

        private void ViewModel_LoginSuccess(object sender, EventArgs e)
        {
            _mainViewModel = new MainViewModel(_model);
            _mainViewModel.MessageApplication += new EventHandler<MessageEventArgs>(ViewModel_MessageApplication);
            _mainViewModel.ItemEditingStarted += new EventHandler(MainViewModel_ItemEditingStarted);
            _mainViewModel.ItemEditingFinished += new EventHandler(MainViewModel_ItemEditingFinished);
            _mainViewModel.ItemShowStarted += new EventHandler(MainViewModel_ItemEditingStarted);
            //_mainViewModel.ImageEditingStarted += new EventHandler<ItemEventArgs>(MainViewModel_ImageEditingStarted);
            _mainViewModel.ExitApplication += new EventHandler(ViewModel_ExitApplication);
            _mainViewModel.noItems += new EventHandler(ViewModel_NoItems);
            _mainViewModel.Loaded += new EventHandler(ViewModel_Loaded);

            _mainView = new MainWindow();
            _mainView.DataContext = _mainViewModel;
            _mainView.Show();
            _loginView.Close();
        }

        private void ViewModel_LoginFailed(object sender, EventArgs e)
        {
            MessageBox.Show("A bejelentkezés sikertelen!", "AuctionSite", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void ViewModel_Loaded(object sender, EventArgs e)
        {
            MessageBox.Show("Termék lista frisstve.", "AuctionSite", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void ViewModel_NoItems(object sender, EventArgs e)
        {
            MessageBox.Show("Nincs elérhető termék!", "AuctionSite", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void ViewModel_MessageApplication(object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "AuctionSite", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void MainViewModel_ItemEditingStarted(object sender, EventArgs e)
        {
            _editorView = new ItemEditor(); 
            _editorView.DataContext = _mainViewModel;
            _editorView.Show();
        }

        private void MainViewModel_ItemEditingFinished(object sender, EventArgs e)
        {
            _editorView.Close();
        }
        /*
        private void MainViewModel_ImageEditingStarted(object sender, ItemEventArgs e)
        {
            try
            {
                // egy dialógusablakban bekérjük a fájlnevet
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.CheckFileExists = true;
                dialog.Filter = "Képfájlok|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png;";
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                Boolean? result = dialog.ShowDialog();

                if (result == true)
                {
                    // kép létrehozása (a megfelelő méretekkel)
                    _model.CreateImage(e.ItemId,
                                       ImageHandler.OpenAndResize(dialog.FileName, 100),
                                       ImageHandler.OpenAndResize(dialog.FileName, 600));
                }
            }
            catch { }
        }*/

        private void ViewModel_ExitApplication(object sender, System.EventArgs e)
        {
            Shutdown();
        }
    }
}