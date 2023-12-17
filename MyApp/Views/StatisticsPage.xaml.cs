using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
