using Newtonsoft.Json.Linq;

namespace SEproject.Data
{
    class ImageControl
    {
        Image image;
        ServerConnector serverconnector;
        
        public ImageControl(Image img, ServerConnector sc)
        {
            image = img;
            serverconnector = sc;
        }
        
        public Image getImage()
        {
            return image;
        }
        public int remove()
        {
            JObject result = JObject.Parse(serverconnector.GET("image/rm/" + image.uuid));
            if(result["code"].ToString() == "0")
            {
                return 0;
            }
            return -1;
        }

        public int run()
        {
            JObject result = JObject.Parse(serverconnector.GET("image/run/" + image.uuid + "/" + image.port));
            if (result["code"].ToString() == "0")
            {
                return 0;
            }
            return -1;
        }


    }
}
