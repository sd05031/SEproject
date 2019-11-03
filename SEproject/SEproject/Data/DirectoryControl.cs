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
        private string token;
        private IList<File> Files;
        public string[] File { get; private set; }
        public string[] Directory { get; private set; }

        public DirectoryControl(string t)
        {
            path = ".";
            token = t;
            get_list();
        }

        string POST()
        {
            string url = "http://nekop.kr:3000/api/v1/directory";
            string postdata = "path=" + path;
            byte[] bytearray = Encoding.UTF8.GetBytes(postdata);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytearray.Length;
            request.Timeout = 30 * 1000;
            request.Headers.Add("X-Access-Token", token);

            Stream datastream = request.GetRequestStream();
            datastream.Write(bytearray, 0, bytearray.Length);
            datastream.Close();

            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

            datastream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(datastream);
            string rtext = sr.ReadToEnd();

            return rtext;
        }

        int get_list()
        {
            Files = new List<File>();
            JObject json = JObject.Parse(POST());
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
                    //Directory[i] = directories[i].ToString();
                }

                for (int i = 0; i < files.Count; i++)
                {
                    Files.Add(new Data.File(files[i].ToString(), 0));
                    //File[i] = files[i].ToString();
                }
                return 0;
            }
            return -1;
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
