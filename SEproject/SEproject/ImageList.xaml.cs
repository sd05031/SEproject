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
        IList<SEproject.Data.Image> images;
        public ImageList()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            manage = (Manage)BindingContext;
            manage.update_image();

            images = manage.getImages();
            if (images == null || images.Count <= 0)
            {
                noLabel.IsVisible = true;
                IList.IsVisible = false;
            }
            else
            {
                IList.ItemsSource = manage.getImages();
            }
            base.OnAppearing();
        }
        void OnTapped(object sender, ItemTappedEventArgs e)
        {
            Data.Image Item = e.Item as Data.Image;
            ImageControl IC = new ImageControl(Item, manage.GetServerConnector());
            Navigation.PushAsync(new ImageDetail
            {
                BindingContext = IC
            });
        }
    }
}