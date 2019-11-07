using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text;

namespace SEproject.Data
{
    class ServerConnector
    {
        string ServerUrl;
        string token;
        public ServerConnector(Account acc)
        {
            ServerUrl = "http://nekop.kr:3000/api/v1/";
            token = acc.getToken();
        }

        public string GET(string suburl)
        {
            string url = ServerUrl + suburl;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = 30 * 1000;
            request.Headers.Add("X-Access-Token", token);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            HttpStatusCode status = response.StatusCode;

            Stream readstream = response.GetResponseStream();
            StreamReader sr = new StreamReader(readstream);
            string text = sr.ReadToEnd();

            return text;
        }

        public string POST(string suburl, string POSTMessage)
        {
            string url = ServerUrl + suburl;
            //string postdata = "path=" + path;
            byte[] bytearray = Encoding.UTF8.GetBytes(POSTMessage);

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
    }
}
