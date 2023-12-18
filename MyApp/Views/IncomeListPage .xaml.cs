using MyApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncomeListPage : ContentPage
    {
        public IncomeListPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve expenses from Preferences
            var incomes = Preferences.Get("incomes", string.Empty);

            // Deserialize JSON string to List<Expense>
            var incomeList = string.IsNullOrEmpty(incomes)
                ? new List<ExpenseModel>()
                : JsonConvert.DeserializeObject<List<ExpenseModel>>(incomes);

            // Bind expenseList to a ListView or TableView in XAML
            IncomeListView.ItemsSource = incomeList;
        }
        private void ClearTable()
        {
            // Clear data from Preferences
            Preferences.Remove("incomes");
            Preferences.Remove("totalIncomes");

            // Update the ListView or TableView
            IncomeListView.ItemsSource = new List<IncomeModel>();
        }
        private void ClearTableButton(object sender, EventArgs e)
        {
            ClearTable();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IncomePage());
        }
    }
}