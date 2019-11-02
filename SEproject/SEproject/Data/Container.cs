using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

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

        public Container(Container con)
        {
            this.uid = con.uid;
            this.Tag = con.Tag;
            this.Status = con.Status;
            this.image = con.image;
            this.port = con.port;
            this.short_id = con.short_id;
            this.started_time = con.started_time;
            this.uuid = con.uuid;
        }
    }
}
