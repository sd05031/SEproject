using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SEproject.Data
{
    class Manage
    {
        ServerConnector serverconnector;
        private IList<Container> Containers { get; set; }
        private IList<Image> Images { get; set; }

        public Manage()
        {
            Containers = null;
            Images = null;
        }

        public void setServerConnector(ServerConnector sc)
        {
            serverconnector = sc;
        }
        public ServerConnector GetServerConnector()
        {
            return serverconnector;
        }
        public int update_container()
        {
            Containers = new List<Container>();
            JObject result;
            try
            {
                result = JObject.Parse(serverconnector.GET("containers"));
            }
            catch (Exception ex)
            {
                return -1;
            }

            if (result["code"].ToString() == "0")
            {
                JArray jarray = JArray.Parse(result["msg"].ToString());

                for (int i = 0; i < jarray.Count; i++)
                {
                    Containers.Add(new Container(jarray[i].ToString()));
                }
                return jarray.Count;
            }
            else
            {
                return -1;
            }
        }

        public int update_image()
        {
            Images = new List<Image>();
            JObject result = JObject.Parse(serverconnector.GET("images"));
            if (result["code"].ToString() == "0")
            {
                JArray jarray = JArray.Parse(result["msg"].ToString());

                for (int i = 0; i < jarray.Count; i++)
                {
                    Images.Add(new Image(jarray[i].ToString()));
                }
                return jarray.Count;
            }
            else
            {
                return -1;
            }
        }

        public IList<Container> getContainers()
        {
            if (Containers == null)
            {
                update_container();
            }
            return Containers;
        }
        public IList<Image> getImages()
        {
            if (Images == null)
            {
                update_image();
            }
            return Images;
        }
    }
}
