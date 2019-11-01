using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEproject.Data
{
    class Container
    {
        string uid;
        string tag;
        string status;
        string image;
        int port;
        string short_id;
        string started_time;
        string uuid;


        public Container(string text)
        {
            JObject j = JObject.Parse(text);

            uid = j["uid"].ToString();
            tag = j["tag"].ToString();
            status = j["status"].ToString();
            image = j["image"].ToString();
            port = Int32.Parse(j["port"].ToString());
            short_id = j["short_id"].ToString();
            started_time = j["started_time"].ToString();
            uuid = j["uuid"].ToString();
        }
    }
}
