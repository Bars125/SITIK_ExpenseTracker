using MyApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}