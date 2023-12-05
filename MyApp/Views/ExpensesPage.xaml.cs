using MyApp.ViewModels;
using Newtonsoft.Json;
using SQLite;
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
    public partial class ExpensesPage : ContentPage
    {
        //ExpenseViewModel _viewModel;
        public ExpensesPage()
        {
            InitializeComponent();

           // _viewModel = new ExpenseViewModel();
            //BindingContext = _viewModel;
        }
        protected override void OnAppearing()
        {
            // Set focus on the amountEntry field when the page appears
            amountEntry.Focus();
            base.OnAppearing();
        }
        private void AddExpenseClicked(object sender, EventArgs e)
        {

            // Get user input
            int amount = int.Parse(amountEntry.Text);
            string category = categoryPicker.ToString();

            // Create Expense object
            ExpenseModel expense = new ExpenseModel { Amount = amount, Category = category };

            // Save data using Preferences
            SaveExpense(expense);

            // Clear input fields
            amountEntry.Text = string.Empty;
            category = string.Empty;

            // order is important!!
            ((Button)sender).Text = "Done!";
            ((Button)sender).BackgroundColor = Color.Coral;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                ((Button)sender).Text = "Add expense";
                ((Button)sender).BackgroundColor = Color.FromHex("#2196FF");
                return false; // Остановить таймер
            });
        }
        private void SaveExpense(ExpenseModel expense)
        {
            // Retrieve existing expenses
            var expenses = Preferences.Get("expenses", string.Empty);

            // Deserialize JSON string to List<Expense>
            var expenseList = string.IsNullOrEmpty(expenses)
                ? new List<ExpenseModel>()
                : JsonConvert.DeserializeObject<List<ExpenseModel>>(expenses);

            // Add new expense
            expenseList.Add(expense);

            // Serialize List<Expense> to JSON string and save to Preferences
            Preferences.Set("expenses", JsonConvert.SerializeObject(expenseList));
        }
        private void ExpenseListClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ExpensesListPage());
        }
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InitialPage());
        }
    }
}