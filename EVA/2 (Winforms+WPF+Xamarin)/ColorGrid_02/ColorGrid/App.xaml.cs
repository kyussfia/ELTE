using ELTE.Windows.ColorGrid.View;
using ELTE.Windows.ColorGrid.ViewModel;
using System.Windows;

namespace ELTE.Windows.ColorGrid
{
    public partial class App : Application
    {
        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            MainWindow window = new MainWindow();

            ColorGridViewModel viewModel = new ColorGridViewModel();
            window.DataContext = viewModel;

            window.Show();
        }
    }
}
