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
    public partial class ImageList : ContentPage
    {
        Manage manage;
        public ImageList()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            manage = (Manage)BindingContext;
            IList.ItemsSource = manage.getImages();
            base.OnAppearing();
        }
    }
}