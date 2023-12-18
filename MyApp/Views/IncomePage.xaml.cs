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
        private void AddIncomeClicked(object sender, EventArgs e)
        {

            // Get user input and validation
            if (double.TryParse(amountEntry.Text, out double amount) && amount > 0)
            {
                amount = double.Parse(amountEntry.Text);
                string category = CategoryPicker.SelectedItem != null ? CategoryPicker.SelectedItem.ToString() : "General";

                // Create Income object
                IncomeModel income = new IncomeModel { Amount = amount, Category = category };

                // Save data using Preferences
                SaveIncome(income);

                // Clear input fields
                amountEntry.Text = string.Empty;
                category = string.Empty;

                // order is important!!
                ((Button)sender).Text = "Done!";
                ((Button)sender).BackgroundColor = Color.Coral;
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    ((Button)sender).Text = "Add income";
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
                    ((Button)sender).Text = "Add income";
                    ((Button)sender).BackgroundColor = Color.FromHex("#1b32a4");
                    return false; // Остановить таймер
                });
            }
        }
        private void SaveIncome(IncomeModel income)
        {
            // Retrieve existing incomes
            var incomes = Preferences.Get("incomes", string.Empty);

            // Deserialize JSON string to List<Income>
            var incomeList = string.IsNullOrEmpty(incomes)
                ? new List<IncomeModel>()
                : JsonConvert.DeserializeObject<List<IncomeModel>>(incomes);

            // Add new income
            incomeList.Add(income);

            // Serialize List<Income> to JSON string and save to Preferences
            Preferences.Set("incomes", JsonConvert.SerializeObject(incomeList));

            // Update the sum of incomes
            double totalIncomes = incomeList.Sum(x => x.Amount);
            Preferences.Set("totalIncomes", totalIncomes);
        }
        private void IncomeListClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new IncomeListPage());
        }
        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}