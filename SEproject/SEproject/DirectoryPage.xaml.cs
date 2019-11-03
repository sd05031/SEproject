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
        IList<string> file;
        IList<string> dir;
        IList<Data.File> files;
        public DirectoryPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            DC = (DirectoryControl)BindingContext;
            get_list();
            base.OnAppearing();
        }
        void get_list()
        {
            files = DC.GetFiles();
            DirList.ItemsSource = files;
            path_Label.Text = DC.getpath();
        }
        void OnTapped(Object sender, SelectedItemChangedEventArgs e)
        {
            Data.File item = e.SelectedItem as Data.File;

            if ( item.Is_directory == 1 )
            {
                DC.movepath(item.Name);
                get_list();
            }
            else
            {
                DisplayAlert("FileSelected", item.Name, "OK");
            }
            
        }
    }
}