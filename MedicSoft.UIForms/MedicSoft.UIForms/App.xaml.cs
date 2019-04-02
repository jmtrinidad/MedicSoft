using MedicSoft.UIForms.View;
using MedicSoft.UIForms.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MedicSoft.UIForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainViewModel.GetInstance().Products = new ProductsViewModel();
            this.MainPage = new NavigationPage(new ProductsPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
