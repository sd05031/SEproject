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
        public DirectoryPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            DC = (DirectoryControl)BindingContext;
            string[] sample_file = { "a.txt", "b.txt", "abc.avi", "ggg.exe", "T.mp4","toe.mp3","ette.zip" };
            string[] sample_dir = { "rootfolder", "data", "source", "src", "bin", "gos", "soee" };
            //file = DC.File.ToList();
            //dir = DC.Directory.ToList();
            file = sample_file.ToList();
            dir = sample_dir.ToList();

            DirList.ItemsSource = dir;
            FList.ItemsSource = file;
            
            base.OnAppearing();
        }
    }
}