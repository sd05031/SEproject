using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SEproject.Data
{
    class Account
    {
        //private string token;
        string token;
        Container[] containers;
        Image[] images;
        string path;

        public Account()
        {
            containers = null;
            images = null;
            path = ".";
        }

        public int Login(string id, string pw)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri("http://nekop.kr:3000/api/v1/request_auth");
            KeyValuePair<string, string>[] pair = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("username",id),
                new KeyValuePair<string, string>("password",pw)
            };

            var form = new FormUrlEncodedContent(pair);
            var response = client.PostAsync(uri, form).Result;

            var result = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            if ( result["code"].ToString() == "0")
            {
                token = result["msg"]["token"].ToString();
                return 0;
            }
            return -1;
        }

        private string GET(string suburl)
        {
            string url = "http://nekop.kr:3000/api/v1/";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + suburl);
            request.Method = "GET";
            request.Timeout = 30 * 1000;
            request.Headers.Add("X-Access-Token", token);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            HttpStatusCode status = response.StatusCode;

            Stream rs = response.GetResponseStream();
            StreamReader sr = new StreamReader(rs);
            string text = sr.ReadToEnd();

            return text;
        }
        public int update_container()
        {
            JObject result = JObject.Parse(GET("containers"));
            if( result["code"].ToString() == "0")
            {
                JArray jarray = JArray.Parse(result["msg"].ToString());
                containers = new Container[jarray.Count];

                for ( int i = 0; i < jarray.Count; i ++)
                {
                    containers[i] = new Container(jarray[i].ToString());
                }
                return jarray.Count;

            }
            else
            {
                return -1;
            }
        }

        public int update_image()
        {
            JObject result = JObject.Parse(GET("images"));
            if ( result["code"].ToString() == "0" )
            {
                JArray jarray = JArray.Parse(result["msg"].ToString());
                images = new Image[jarray.Count];

                for (int i = 0; i < jarray.Count; i++)
                {
                    images[i] = new Image(jarray[i].ToString());
                }
                return jarray.Count;
            }
            else
            {
                return -1;
            }
        }

        public Container[] getContainers()
        {
            return containers;
        }

        public Image[] getImages()
        {
            return images;
        }
    }
}
