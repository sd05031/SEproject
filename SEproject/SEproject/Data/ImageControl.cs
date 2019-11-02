using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SEproject.Data
{
    class ImageControl
    {
        Image image;
        private string token;
        
        public ImageControl(Image img, string t)
        {
            image = img;
            token = t;
        }
        
        public Image getImage()
        {
            return image;
        }
        private string GET(string suburl)
        {
            string url = "http://nekop.kr:3000/api/v1/image";
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
            string r = GET("/rm/" + image.uuid);
            JObject result = JObject.Parse(r);
            if(result["code"].ToString() == "0")
            {
                return 0;
            }
            return -1;
        }

        int run()
        {
            string r = GET("/run/" + image.uuid + "/" + image.port);
            JObject result = JObject.Parse(r);
            if (result["code"].ToString() == "0")
            {
                return 0;
            }
            return -1;
        }


    }
}
