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

        public ContainerControl(Container con, string t)
        {
            container = con;
            token = t;
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
            //TODO
            return 0;
        }

        int start()
        {
            //TODO
            return 0;
        }

        int stop()
        {
            //TODO
            return 0;
        }
    }
}
