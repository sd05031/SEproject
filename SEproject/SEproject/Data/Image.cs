using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEproject.Data
{
    public class Image
    {
        string uid;
        string os;
        string tag;
        int port;
        string status;
        string short_id;
        string uuid;

        public Image(string text)
        {
            JObject j = JObject.Parse(text);

            uid = j["uid"].ToString();
            os = j["os"].ToString();
            tag = j["tag"].ToString();
            port = Int32.Parse(j["port"].ToString());
            status = j["status"].ToString();
            short_id = j["short_id"].ToString();
            uuid = j["uuid"].ToString();
        }
    }
}
