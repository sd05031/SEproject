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
        public LoginedPage()
        {
            InitializeComponent();
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

        }

        private void showDirectory(object sender, EventArgs e)
        {

        }
    }
}