using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


using SEproject.Data;

namespace SEproject
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Account account = new Account();
        public MainPage()
        {
            InitializeComponent();
        }

        void OnLoginButton(Object sender, EventArgs e)
        {
            int result = account.Login(username_entry.Text.ToString(), password_entry.Text.ToString());
            if ( result == 0)
            {
                Application.Current.MainPage = new NavigationPage(new LoginedPage
                {
                    BindingContext = account
                });
            }
            else
            {
                DisplayAlert("Notice", "Login Failed", "OK");
            }
        }
    }
}
