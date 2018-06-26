using System;
using ELTE.ColorGrid.View;
using ELTE.ColorGrid.ViewModel;
using Xamarin.Forms;

namespace ELTE.ColorGrid
{
    public class App : Application
    {
        public App()
        {
            // nézetmodell létrehozása
            ColorGridViewModel viewModel = new ColorGridViewModel();

            MainPage = new MainPage();
            MainPage.BindingContext = viewModel; // nézetmodell befecskendezése
        }
    }
}
