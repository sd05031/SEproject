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

        public DirectoryControl(string t)
        {
            token = t;
        }

        int get()
        {
            string url = "http://nekop.kr:3000/api/v1/directory";
            string postdata = "path=.";
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
        }

    }
}
