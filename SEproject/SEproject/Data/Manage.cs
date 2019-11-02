﻿using Newtonsoft.Json.Linq;
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
        private IList<Container> containers { get; set; }
        Image[] images;
        string path;

        public Manage()
        {
            containers = null;
            images = null;
            path = ".";
        }

        public string gettoken()
        {
            return account.getToken();
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
        public int update_container()
        {
            containers = new List<Container>();
            JObject result = JObject.Parse(GET("containers"));
            if (result["code"].ToString() == "0")
            {
                JArray jarray = JArray.Parse(result["msg"].ToString());

                for (int i = 0; i < jarray.Count; i++)
                {
                    containers.Add(new Container(jarray[i].ToString()));
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
            if (result["code"].ToString() == "0")
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

        public IList<Container> getContainers()
        {
            update_container();
            return containers;
        }
        public Image[] getImages()
        {
            if (images == null)
            {
                update_image();
            }
            return images;
        }
    }
}
