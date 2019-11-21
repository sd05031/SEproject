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

        async void RunButtonClicked(Object sender, EventArgs e)
        {
            var result = await DisplayAlert("Notice", "이미지를 실행 하시겠습니까?", "YES", "NO");
            if (result)
            {
                if ( IC.run() == 0)
                {
                    await DisplayAlert("Notice", "이미지가 성공적으로 실행되었습니다", "확인");
                    RunButton.IsEnabled = false;
                }
                else
                    await DisplayAlert("Notice", "이미지 실행을 실패하였습니다.", "확인");
            }
        }

        async void DeleteButton(Object sender, EventArgs e)
        {
            var result = await DisplayAlert("Notice", "정말 삭제 하시겠습니까?\n삭제된 이미지는 복구가 불가능합니다.", "YES", "NO");
            if (result)
            {
                if ( /*IC.remove() == 0 */ false) 
                {
                    await DisplayAlert("Notice", "이미지가 성공적으로 삭제 되었습니다", "확인");
                    await Navigation.PopAsync();
                }
                else
                    await DisplayAlert("Notice", "이미지 삭제에 실패하였습니다", "확인");
            }
        }
    }
}