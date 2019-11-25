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
        Manage manage;
        ServerConnector SC;
        public LoginedPage()
        {
            InitializeComponent();
            manage = new Manage();
        }

        protected override void OnAppearing()
        {
            SC = (ServerConnector)BindingContext;
            manage.setServerConnector(SC);
            base.OnAppearing();
        }

        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
            return true;
        }

        private void getImages(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ImageList
            {
                BindingContext = manage
            });
        }
        private void getContainers(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ContainerList
            {
                BindingContext = manage
            });
        }

        private void showDirectory(object sender, EventArgs e)
        {
            DirectoryControl DC = new DirectoryControl(SC);
            Navigation.PushAsync(new DirectoryPage
            {
                BindingContext = DC
            });
        }

        private void LogOut(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }
    }
}