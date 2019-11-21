using Newtonsoft.Json.Linq;
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
    public partial class FileDetail : ContentPage
    {
        string[] data;
        public FileDetail()
        {
            InitializeComponent();
        }

        void filecheck()
        {
            string[] list = { "txt" };
            string extend = data[1].Substring(data[1].IndexOf('.'));
            if ( list.Contains(extend) )
            {
                DisplayAlert("Notice", "내용", "확인");
                string path = data[0] + '/' + data[1];
                JObject json = DirectoryControl.download(path);
                if (Int32.Parse(json["code"].ToString()) == 0)
                {
                    neyoung.Text = json["msg"].ToString();
                }
                else
                {
                    neyoung.Text = "데이터를 불러오는데 실패하였습니다.";
                }
            }
            else
            {
                neyoung.Text = "미리보기를 지원하지 않는 파일 형식입니다";
            }
        }
        protected override void OnAppearing()
        {
            data = (string[])BindingContext;
            PathLabel.Text = data[0];
            FNameLabel.Text = data[1]; 

            base.OnAppearing();
        }

        
    }
}