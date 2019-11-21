using Newtonsoft.Json.Linq;

namespace SEproject.Data
{
    class ContainerControl
    {
        Container container;
        ServerConnector serverconnector;
        
        public ContainerControl(Container container, ServerConnector sc)
        {
            this.container = container;
            serverconnector = sc;
        }

        public Container getContainer()
        {
            return container;
        }

        public int remove()
        {
            string r = serverconnector.GET("container/rm/" + container.uuid);
            JObject result = JObject.Parse(r);
            if ( result["code"].ToString() == "0")
            {
                return 0;
            }
            return -1;
        }

        public int start()
        {
            string r = serverconnector.GET("container/start/" + container.uuid);
            JObject result = JObject.Parse(r);
            if (result["code"].ToString() == "0")
            {
                return 0;
            }
            return -1;
        }

        public int stop()
        {
            string r = serverconnector.GET("container/stop/" + container.uuid);
            JObject result = JObject.Parse(r);
            if(result["code"].ToString() == "0")
            {
                return 0;
            }
            return -1;
        }
    }
}
