using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEproject.Data
{
    public class Image
    {
        public string uid;
        public string os;
        public string Tag { get; set; }
        int port;
        public string Status { get; set; }
        public string short_id;
        public string uuid;

        public Image(string text)
        {
            JObject j = JObject.Parse(text);

            uid = j["uid"].ToString();
            os = j["os"].ToString();
            Tag = j["tag"].ToString();
            port = Int32.Parse(j["port"].ToString());
            Status = j["status"].ToString();
            short_id = j["short_id"].ToString();
            uuid = j["uuid"].ToString();
        }
    }
}
