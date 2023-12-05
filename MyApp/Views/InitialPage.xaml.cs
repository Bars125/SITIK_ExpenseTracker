using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.Views
{
    public partial class InitialPage : ContentPage
    {
        public InitialPage()
        {
            InitializeComponent();
        }
        private async void ExpensesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExpensesPage());
        }
        private async void IncomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IncomePage());
        }
        private async void StatisticsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatisticsPage());
        }
    }
}
