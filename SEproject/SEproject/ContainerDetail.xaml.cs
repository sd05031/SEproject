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
    public partial class ContainerDetail : ContentPage
    {
        ContainerControl CC;
        Container con;
        public ContainerDetail()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            CC = (ContainerControl)BindingContext;
            con = CC.getContainer();

            uidLabel.Text = con.uid;
            tagLabel.Text = con.Tag;
            statusLabel.Text = con.Status;
            imageLabel.Text = con.image;
            portLabel.Text = con.port.ToString();
            shortLabel.Text = con.short_id;
            startLabel.Text = con.started_time;
            uuidLabel.Text = con.uuid;
            
            if(con.Status == "start")
            {
                StartButton.IsEnabled = false;
                RemoveButton.IsEnabled = false;
                StopButton.IsEnabled = true;
            }
            else
            {
                StartButton.IsEnabled = true;
                RemoveButton.IsEnabled = true;
                StopButton.IsEnabled = false;
            }

            base.OnAppearing();
        }

        async void RemoveButtonClicked(Object sender, EventArgs e)
        {
            var result = await DisplayAlert("Notice", "해당 컨테이너를 삭제하시겠습니까?", "YES", "NO");
            if (result)
            {
                if ( CC.remove() == 0 )
                {
                    await DisplayAlert("Notice", "컨테이너가 성공적으로 삭제되었습니다", "확인");
                    Navigation.PopAsync();
                }
                else
                    await DisplayAlert("Notice", "컨테이너 삭제에 실패하였습니다", "확인");
            }
        }

        async void StartButtonClicked(Object sender, EventArgs e)
        {
            var result = await DisplayAlert("Notice", "컨테이너를 시작 하시겠습니까?", "YES", "NO");
            if (result)
            {
                if (CC.start() == 0)
                {
                    await DisplayAlert("Notice", "컨테이너가 성공적으로 시작되었습니다", "확인");
                    StartButton.IsEnabled = false;
                    RemoveButton.IsEnabled = false;
                    StopButton.IsEnabled = true;
                }
                else
                    await DisplayAlert("Notice", "컨테이너 시작을 실패하였습니다", "확인");
            }
        }

        async void StopButtonClicked(Object sender, EventArgs e)
        {
            var result = await DisplayAlert("Notice", "컨테이너를 멈추시겠습니까?", "YES", "NO");
            if (result)
            {
                if (CC.stop() == 0)
                {
                    await DisplayAlert("Notice", "컨테이너가 중지 되었습니다", "확인");
                    StartButton.IsEnabled = true;
                    RemoveButton.IsEnabled = true;
                    StopButton.IsEnabled = false;
                }
                else
                    await DisplayAlert("Notice", "컨테이너 중지에 실패하였습니다", "확인");
            }
        }
    }
}