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
        public LoginedPage()
        {
            InitializeComponent();
            manage = new Manage();
        }

        protected override void OnAppearing()
        {
            manage.setAccount((Account)BindingContext);
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
            DirectoryControl DC = new DirectoryControl(manage.getToken());
            Navigation.PushAsync(new DirectoryPage
            {
                BindingContext = DC
            });
        }
    }
}