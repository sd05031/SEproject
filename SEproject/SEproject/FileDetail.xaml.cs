using Newtonsoft.Json.Linq;
using SEproject.Data;
using System;
using System.IO;
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
        DirectoryControl DC;
        Object[] datas;
        string[] data;
        string file_data;
        public FileDetail()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            datas = (Object[])BindingContext;
            data = new string[2];
            data[0] = datas[0] as string;
            data[1] = datas[1] as string;
            PathLabel.Text = data[0];
            FNameLabel.Text = data[1];
            DC = datas[2] as DirectoryControl;

            filecheck();
            base.OnAppearing();
        }
        
        void filecheck()
        {
            string[] list = { "txt" };
            string extend = data[1].Substring(data[1].Length-3);
            if ( list.Contains(extend) )
            {
                string path = data[0] + '/' + data[1];
                JObject json = DC.download(path);
                if (Int32.Parse(json["code"].ToString()) == 0)
                {
                    file_data = json["msg"].ToString();
                    FileData.Text = file_data;
                }
                else
                {
                    FileData.Text = "데이터를 불러오는데 실패하였습니다.";
                }
            }
            else
            {
                FileData.Text = "미리보기를 지원하지 않는 파일 형식입니다";
                file_data = null;
            }
        }

        private void Download_Button_Clicked(object sender, EventArgs e)
        {
            string folderpath = System.IO.Path.Combine(App.SDCardPath, "SEFolder");
            if (!System.IO.Directory.Exists(folderpath))
            {
                System.IO.Directory.CreateDirectory(folderpath);
            }
            
            if (file_data == null)
            {
                file_data = DC.download(data[0] + '/' + data[1])["msg"].ToString();
                System.IO.File.WriteAllBytes(System.IO.Path.Combine(folderpath, data[1]), Encoding.UTF8.GetBytes(file_data));
                DisplayAlert("Notice","파일의 다운로드가 완료 되었습니다","OK");
            }
            else
            {
                System.IO.File.WriteAllText(System.IO.Path.Combine(folderpath, data[1]), file_data);
                DisplayAlert("Notice", "파일의 다운로드가 완료 되었습니다", "OK");
            }
        }
    }
}