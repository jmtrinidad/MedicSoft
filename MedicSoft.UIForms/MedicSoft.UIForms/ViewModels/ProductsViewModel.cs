namespace MedicSoft.UIForms.ViewModels
{
    using MedicSoft.Common.Models;
    using MedicSoft.Common.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    //, StringFormat= 'Price: {0:C2}' 
    //, StringFormat='Stock: {0:N2}' 
    public class ProductsViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        private ObservableCollection<Product> products;
        private bool isRefreshing;

        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetValue(ref isRefreshing, value);
        }
        public ObservableCollection<Product> Products
        {
            get => products;
            set => SetValue(ref products, value);
        }

        public ProductsViewModel()
        {
            apiService = new ApiService();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;
            Response response = await apiService.GetListAsync<Product>(
                "https://shopzulu.azurewebsites.net",
                "/api",
                "/Products"
                );
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            List<Product> myProduct = (List<Product>)response.Result;

            Products = new ObservableCollection<Product>(myProduct);

            this.IsRefreshing = false;
        }
    }
}
