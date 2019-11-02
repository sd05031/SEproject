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
    public partial class DirectoryPage : ContentPage
    {
        DirectoryControl DC;
        public DirectoryPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            DC = (DirectoryControl)BindingContext;
            base.OnAppearing();
        }
    }
}