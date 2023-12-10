using MyApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncomePage : ContentPage
    {
        public IncomePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            // Set focus on the amountEntry field when the page appears
            amountEntry.Focus();
            base.OnAppearing();
        }
        private void AddIncomeClicked(object sender, EventArgs e)
        {

            // Get user input and validation
            if (double.TryParse(amountEntry.Text, out double amount) && amount > 0)
            {
                amount = double.Parse(amountEntry.Text);
                string category = CategoryPicker.SelectedItem != null ? CategoryPicker.SelectedItem.ToString() : "General";

                // Create Expense object
                IncomeModel expense = new IncomeModel { Amount = amount, Category = category };

                // Save data using Preferences
                SaveIncome(expense);

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
            else
            {
                ((Button)sender).Text = "Enter Amount!";
                ((Button)sender).BackgroundColor = Color.FromHex("#c71906");
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    ((Button)sender).Text = "Add expense";
                    ((Button)sender).BackgroundColor = Color.FromHex("#2196FF");
                    return false; // Остановить таймер
                });
            }
        }
        private void SaveIncome(IncomeModel expense)
        {
            // Retrieve existing expenses
            var expenses = Preferences.Get("incomes", string.Empty);

            // Deserialize JSON string to List<Expense>
            var expenseList = string.IsNullOrEmpty(expenses)
                ? new List<IncomeModel>()
                : JsonConvert.DeserializeObject<List<IncomeModel>>(expenses);

            // Add new expense
            expenseList.Add(expense);

            // Serialize List<Expense> to JSON string and save to Preferences
            Preferences.Set("incomes", JsonConvert.SerializeObject(expenseList));
        }
        private void IncomeListClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ExpensesListPage());
        }
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InitialPage());
        }
    }
}