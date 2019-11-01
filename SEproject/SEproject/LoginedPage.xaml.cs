using SEproject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SEproject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginedPage : ContentPage
    {
        Account account;
        public LoginedPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            account = (Account)BindingContext;
            base.OnAppearing();
        }

        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
            return true;
            //return base.OnBackButtonPressed();
        }

        private void getImages(object sender, EventArgs e)
        {

        }
        private void getContainers(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ContainerList
            {
                BindingContext = account
            });
        }

        private void showDirectory(object sender, EventArgs e)
        {

        }
    }
}