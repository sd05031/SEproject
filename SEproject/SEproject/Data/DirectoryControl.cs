using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SEproject.Data
{
    class DirectoryControl
    {
        string path;
        ServerConnector serverconnector;
        private IList<File> Files;

        public DirectoryControl(ServerConnector sc)
        {
            path = ".";
            serverconnector = sc;
            get_list();
        }

        int removeFile(string filename)
        {
            string postdata = "path=" + path + "/" + filename;
            string rtext = serverconnector.POST("directory/remove", postdata);

            JObject jobject = JObject.Parse(rtext);
            int result = Int32.Parse(jobject["code"].ToString());

            return result;
        }

        int removeDir()
        {
            if(path.Length < 2)
            {
                return -1;
            }

            string postdata = "path=" + path;
            string rtext = serverconnector.POST("directory/removedir", postdata);
            
            JObject jobject = JObject.Parse(rtext);
            int result = Int32.Parse(jobject["code"].ToString());

            return result;
        }

        int get_list()
        {
            Files = new List<File>();

            string postdata = "path=" + path;
            string rtext = serverconnector.POST("directory", postdata);

            JObject json = JObject.Parse(rtext);
            if (json["code"].ToString() == "0")
            {
                var msg = json["msg"];
                var directory_list = msg["dir"].ToString();
                var file_list = msg["file"].ToString();

                JArray directories = JArray.Parse(directory_list);
                JArray files = JArray.Parse(file_list);

                path = msg["path"].ToString().Substring(6);
                if ( path.Length > 2)
                {
                    Files.Add(new Data.File("..", 1));
                }
                for (int i = 0; i < directories.Count; i++)
                {
                    Files.Add(new Data.File(directories[i].ToString(), 1));
                }

                for (int i = 0; i < files.Count; i++)
                {
                    Files.Add(new Data.File(files[i].ToString(), 0));
                }
                return 0;
            }
            return -1;
        }

        static public JObject download(string filepath)
        {
            string url = "http://nekop.kr:3000/api/v1/directory/download";
            string postdata = "path=" + filepath;
            byte[] bytearray = Encoding.UTF8.GetBytes(postdata);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytearray.Length;
            request.Timeout = 30 * 1000;
            //request.Headers.Add("X-Access-Token", sc.gettoken());

            Stream datastream = request.GetRequestStream();
            datastream.Write(bytearray, 0, bytearray.Length);
            datastream.Close();

            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

            datastream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(datastream);
            string rtext = sr.ReadToEnd();

            JObject json = JObject.Parse(rtext);
            //int result = Int32.Parse(json["code"].ToString());
            string value = json["msg"].ToString();
            json["msg"] = Base64Decoder(value);

            return json;
        }
        static private string Base64Decoder(string Base64text)
        {
            System.Text.Encoding encoding;
            encoding = System.Text.Encoding.UTF8;

            byte[] arr = System.Convert.FromBase64String(Base64text);
            return encoding.GetString(arr);

        }
        public void upload()
        {

        }
        public void movepath(string dir)
        {
            if (dir == "..")
            {
                var splited = path.Split('/');
                string newpath = "";
                for ( int i = 0; i < splited.Length - 1; i ++)
                {
                    newpath += i == 0 ? splited[i] : "/" + splited[i];
                }
                path = newpath;
            }
            else
            {
                path = path + "/" + dir;
            }
            get_list();
        }

        public IList<File> GetFiles()
        {
            return Files;
        }
        public string getpath()
        {
            return path;
        }
    }
}
