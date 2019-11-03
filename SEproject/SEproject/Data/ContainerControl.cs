using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SEproject.Data
{
    class ContainerControl
    {
        Container container;
        private string token;

        public ContainerControl(Container container, string token)
        {
            this.container = container;
            this.token = token;
        }

        public Container getContainer()
        {
            return container;
        }

        private string GET(string suburl)
        {
            string url = "http://nekop.kr:3000/api/v1/container";
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

        int remove()
        {
            string r = GET("/rm/" + container.uuid);
            JObject result = JObject.Parse(r);
            if ( result["code"].ToString() == "0")
            {
                return 0;
            }
            return -1;
        }

        int start()
        {
            string r = GET("/start/" + container.uuid);
            JObject result = JObject.Parse(r);
            if (result["code"].ToString() == "0")
            {
                return 0;
            }
            return -1;
        }

        int stop()
        {
            string r = GET("/stop/" + container.uuid);
            JObject result = JObject.Parse(r);
            if(result["code"].ToString() == "0")
            {
                return 0;
            }
            return -1;
        }
    }
}
