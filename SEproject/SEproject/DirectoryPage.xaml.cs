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
        void OnTapped(Object sender, ItemTappedEventArgs e)
        {
            Data.File item = e.Item as Data.File;

            if (item.Is_directory == 1)
            {
                DC.movepath(item.Name);
                get_list();
            }
            else
            {
                DisplayAlert("FileSelected", item.Name, "OK");
            }
        }
        async void DeleteButton(Object sender, EventArgs e)
        {
            var result = await DisplayAlert("Notice", "정말 이 폴더를 삭제하시겠습니까?\n삭제후에는 복구가 불가능합니다.", "YES", "NO");
            if ( result)
            {
                await DisplayAlert("Notice", "삭제합니다", "OK");
                //removeDir result=-1 >> root folder //
            }
        }
    }
}