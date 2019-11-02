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
    public partial class ContainerList : ContentPage
    {
        Manage manage;
        public ContainerList()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            manage = (Manage)BindingContext;
            CList.ItemsSource = manage.getContainers();
            base.OnAppearing();
        }
        void OnTapped(object sender, SelectedItemChangedEventArgs e)
        {
            Container Item = e.SelectedItem as Container;

            DisplayAlert("Notice", Item.uid, "OK!");
        }
    }
}