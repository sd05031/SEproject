﻿using Newtonsoft.Json.Linq;
using System;

namespace SEproject.Data
{
    public class Container
    {
        public string uid;
        public string Tag { get; set; }
        public string Status { get; set; }
        public string image;
        public int port;
        public string short_id;
        public string started_time;
        public string uuid;

        public Container(string text)
        {
            JObject j = JObject.Parse(text);

            uid = j["uid"].ToString();
            Tag = j["tag"].ToString();
            Status = j["status"].ToString();
            image = j["image"].ToString();
            port = Int32.Parse(j["port"].ToString());
            short_id = j["short_id"].ToString();
            started_time = j["started_time"].ToString();
            uuid = j["uuid"].ToString();
        }
    }
}
