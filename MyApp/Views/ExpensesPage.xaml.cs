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
        public ExpensesPage()
        {
            InitializeComponent();
        }
        private void AddExpenseClicked(object sender, EventArgs e)
        {

            // Get user input and validation
            if (double.TryParse(amountEntry.Text, out double amount) && amount > 0)
            {
                amount = double.Parse(amountEntry.Text);
                string category = CategoryPicker.SelectedItem != null ? CategoryPicker.SelectedItem.ToString() : "General";

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
                    ((Button)sender).BackgroundColor = Color.FromHex("#1b32a4");
                    return false; // Остановить таймер
                });
            }
            else
            {
                ((Button)sender).Text = "Enter Amount!";
                ((Button)sender).BackgroundColor = Color.FromHex("#c71906");
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    ((Button)sender).Text = "Add expense";
                    ((Button)sender).BackgroundColor = Color.FromHex("#1b32a4");
                    return false; // Остановить таймер
                });
            }
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

            // Update the sum of expenses
            double totalExpenses = expenseList.Sum(x => x.Amount);
            Preferences.Set("totalExpenses", totalExpenses);
        }
        private void ExpenseListClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ExpensesListPage());
        }
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}