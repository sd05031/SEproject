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
    class sample
    {
        public string tag;
        public string status;
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContainerList : ContentPage
    {
        Manage manage;
        public IList<Container> Containers { get; private set; }
        public ContainerList()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            manage = (Manage)BindingContext;
            Containers = new List<Container>();
            
            manage.getContainers();
            foreach(var c in manage.getContainers())
            {
                Containers.Add(c);
            }
            BindingContext = this;
            base.OnAppearing();
        }
    }
}