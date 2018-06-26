using System;
using ELTE.Calculator.Model;
using ELTE.Calculator.View;
using ELTE.Calculator.ViewModel;
using Xamarin.Forms;

namespace ELTE.Calculator
{
    public class App : Application
    {
        public App()
        {
            // model és nézetmodell létrehozása
            CalculatorModel model = new CalculatorModel();
            CalculatorViewModel viewModel = new CalculatorViewModel(model);
            viewModel.ErrorOccured += new EventHandler<ErrorMessageEventArgs>(ViewModel_ErrorOccured);

            MainPage = new MainPage();
            MainPage.BindingContext = viewModel; // nézetmodell befecskendezése
        }

        private async void ViewModel_ErrorOccured(object sender, ErrorMessageEventArgs e)
        {
            // hibaüzenet megjelenítése
            await MainPage.DisplayAlert("Calculator", e.Message, "Correct");
        }
    }
}
