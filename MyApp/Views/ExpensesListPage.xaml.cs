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
    public partial class ExpensesListPage : ContentPage
    {
        public ExpensesListPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve expenses from Preferences
            var expenses = Preferences.Get("expenses", string.Empty);

            // Deserialize JSON string to List<Expense>
            var expenseList = string.IsNullOrEmpty(expenses)
                ? new List<ExpenseModel>()
                : JsonConvert.DeserializeObject<List<ExpenseModel>>(expenses);

            // Bind expenseList to a ListView or TableView in XAML
            ExpenseListView.ItemsSource = expenseList;
        }
        private void ClearTable()
        {
            // Clear data from Preferences
            Preferences.Remove("expenses");

            // Update the ListView or TableView
            ExpenseListView.ItemsSource = new List<ExpenseModel>();
        }
        private void ClearTableButton(object sender, EventArgs e)
        {
            ClearTable();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExpensesPage());
        }
    }
}