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
        void OnTapped(object sender, ItemTappedEventArgs e)
        {
            Container Item = e.Item as Container;
            ContainerControl CC = new ContainerControl(Item, manage.gettoken());
            Navigation.PushAsync(new ContainerDetail
            {
                BindingContext = CC
            });
        }
    }
}