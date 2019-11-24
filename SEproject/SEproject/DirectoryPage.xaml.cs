using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
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
                Object[] datas = new object[3];
                datas[0] = DC.getpath();
                datas[1] = item.Name;
                datas[2] = DC;
                
                Navigation.PushAsync(new FileDetail{
                    BindingContext = datas
                });
            }
        }
        async void DeleteButton(Object sender, EventArgs e)
        {
            var result = await DisplayAlert("Notice", "정말 이 폴더를 삭제하시겠습니까?\n삭제후에는 복구가 불가능합니다.", "YES", "NO");
            if ( result)
            {
                int value = DC.removeDir();
                if ( value == 0)
                {
                    await DisplayAlert("Notice", "성공적으로 폴더가 삭제 되었습니다", "확인");
                    DC.movepath("..");
                }
                else if ( value == -1)
                {
                    await DisplayAlert("Notice", "root 폴더는 삭제 할 수 없습니다", "확인");
                }
                else
                {
                    await DisplayAlert("Notice", "Error Code = " + value, "확인");
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if(DC.getpath().Length < 2)
            {
                DC.movepath("..");
                return true;
            }
            else
            {
                return base.OnBackButtonPressed();
            }
        }

        async void OnUploadButton(Object sender, EventArgs e)
        {
            FileData fd = await CrossFilePicker.Current.PickFile();
            if (fd == null)
                return;

            DC.upload(fd);
        }
    }
}