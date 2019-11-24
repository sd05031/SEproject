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
        IList<Container> containers;
        public ContainerList()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            manage = (Manage)BindingContext;
            manage.update_container();
            containers = manage.getContainers();

            if(containers == null || containers.Count <= 0)
            {
                noLabel.IsVisible = true;
                CList.IsVisible = false;
            }
            else
            {
                CList.ItemsSource = containers;
            }
            base.OnAppearing();
        }
        void OnTapped(object sender, ItemTappedEventArgs e)
        {
            Container Item = e.Item as Container;
            ContainerControl CC = new ContainerControl(Item, manage.GetServerConnector());
            Navigation.PushAsync(new ContainerDetail
            {
                BindingContext = CC
            });
        }
    }
}