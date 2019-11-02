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

            base.OnAppearing();
        }

        async void RemoveButton(Object sender, EventArgs e)
        {
            var result = await DisplayAlert("Notice", "컨테이너를 삭제하시겠습니까?", "YES", "NO");
            if (result)
            {
                DisplayAlert("Ye-ah!", "Sak-Jae-Da!", "OK!");
            }
            else
            {
                DisplayAlert("??", "JJol?", "lin-jung");
            }
        }

        async void StartButton(Object sender, EventArgs e)
        {
            var result = await DisplayAlert("Notice", "컨테이너를 시작 하시겠습니까?", "YES", "NO");
            if (result)
            {
                DisplayAlert("Notice", "컨테이너가 시작됩니다.", "확인");
            }
        }

        async void StopButton(Object sender, EventArgs e)
        {
            var result = await DisplayAlert("Notice", "컨테이너를 멈추시겠습니까?", "YES", "NO");
            if (result)
            {
                DisplayAlert("Notice", "컨테이너가 멈춥니다.", "확인");
            }
        }
    }
}