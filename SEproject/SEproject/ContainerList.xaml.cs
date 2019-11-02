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

    public class Test
    {
        public string Tag { get; set; }
        public string Status { get; set; }
        public string example1;
    }
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
    }
}