using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticsPage : ContentPage
    {
        private readonly StatisticsVM _viewModel;

        public StatisticsPage()
        {
            InitializeComponent();

            _viewModel = new StatisticsVM();
            BindingContext = _viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            double totalExpenses = Preferences.Get("totalExpenses", 0.0);
            double totalIncomes = Preferences.Get("totalIncomes", 0.0);
            double summary = totalIncomes - totalExpenses;

            Label1.Text = $"Total Expenses: {totalExpenses:C}";
            Label2.Text = $"Total Incomes: {totalIncomes:C}";
            Label3.Text = $"Summary: {summary} $";
        }
    }
}
