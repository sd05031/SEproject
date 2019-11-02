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
    public partial class ImageDetail : ContentPage
    {
        ImageControl IC;
        Data.Image Img;
        public ImageDetail()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            IC = (ImageControl)BindingContext;
            Img = IC.getImage();

            uidLabel.Text = Img.uid;
            osLabel.Text = Img.os;
            tagLabel.Text = Img.Tag;
            portLabel.Text = Img.port.ToString();
            statusLabel.Text = Img.Status;
            shortLabel.Text = Img.short_id;
            uuidLabel.Text = Img.uuid;

            
            base.OnAppearing();
        }

        async void RunButton(Object sender, EventArgs e)
        {
            var result = await DisplayAlert("Notice", "이미지를 실행 하시겠습니까?", "YES", "NO");
            if (result)
            {
                DisplayAlert("Notice", "이미지가 실행됩니다.", "확인");
            }
        }

        async void DeleteButton(Object sender, EventArgs e)
        {
            var result = await DisplayAlert("Notice", "정말 삭제 하시겠습니까?\n삭제된 이미지는 복구가 불가능합니다.", "YES", "NO");
            if (result)
            {
                DisplayAlert("예아", "삭제다!", "확인");
            }
            else
            {
                DisplayAlert("???", "쫄???", "린정");
            }
        }
    }
}