using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SEproject.Data
{
    class Manage
    {
        Account account;
        private IList<Container> Containers { get; set; }
        private IList<Image> Images { get; set; }

        public Manage()
        {
            Containers = null;
            Images = null;
        }

        public void setAccount(Account a)
        {
            account = a;
            update_container();
            update_image();
        }
        private string GET(string suburl)
        {
            string url = "http://nekop.kr:3000/api/v1/";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + suburl);
            request.Method = "GET";
            request.Timeout = 30 * 1000;
            request.Headers.Add("X-Access-Token", account.getToken());

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            HttpStatusCode status = response.StatusCode;

            Stream rs = response.GetResponseStream();
            StreamReader sr = new StreamReader(rs);
            string text = sr.ReadToEnd();

            return text;
        }
        public string getToken()
        {
            return account.getToken();
        }
        public int update_container()
        {
            Containers = new List<Container>();
            JObject result = JObject.Parse(GET("containers"));
            if (result["code"].ToString() == "0")
            {
                JArray jarray = JArray.Parse(result["msg"].ToString());

                for (int i = 0; i < jarray.Count; i++)
                {
                    Containers.Add(new Container(jarray[i].ToString()));
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
            Images = new List<Image>();
            JObject result = JObject.Parse(GET("images"));
            if (result["code"].ToString() == "0")
            {
                JArray jarray = JArray.Parse(result["msg"].ToString());

                for (int i = 0; i < jarray.Count; i++)
                {
                    Images.Add(new Image(jarray[i].ToString()));
                }
                return jarray.Count;
            }
            else
            {
                return -1;
            }
        }

        public IList<Container> getContainers()
        {
            if (Containers == null)
            {
                update_container();
            }
            return Containers;
        }
        public IList<Image> getImages()
        {
            if (Images == null)
            {
                update_image();
            }
            return Images;
        }
    }
}
