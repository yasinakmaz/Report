using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Celerate.Services.DataService;

namespace Celerate.ViewModels
{
    public class CombinedViewModel : INotifyPropertyChanged
    {
        private readonly DataService dataService = new();
        private decimal totalSales;
        private decimal maxSales = 100000;
        private bool isLoading;

        public ObservableCollection<ChartData> ChartItems { get; } = new();

        public decimal TotalSales
        {
            get => totalSales;
            set
            {
                totalSales = value;
                OnPropertyChanged();
            }
        }

        public decimal MaxSales
        {
            get => maxSales;
            set
            {
                maxSales = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }

        public CombinedViewModel()
        {
        }

        public async Task LoadDataAsync()
        {
            IsLoading = true;
            try
            {
                await LoadChartDataAsync();
                await LoadSalesDataAsync();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task LoadChartDataAsync()
        {
            ChartItems.Clear();
            var chartData = await dataService.GetChartDataAsync();

            foreach (var data in chartData)
            {
                ChartItems.Add(data);
            }
        }

        private async Task LoadSalesDataAsync()
        {
            try
            {
                TotalSales = await dataService.GetTotalSalesAsync();
                MaxSales = TotalSales * 1.2m;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Hata", $"Veri yüklenirken hata oluştu: {ex.Message}", "Tamam");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
