using Celerate.ViewModels;

namespace Celerate.Views
{
    public partial class ChartsPage : ContentPage
    {
        public CombinedViewModel ViewModel { get; }
        public ChartsPage()
        {
            InitializeComponent();
            ViewModel = new CombinedViewModel();
            BindingContext = ViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.LoadDataAsync();
        }
    }

}
