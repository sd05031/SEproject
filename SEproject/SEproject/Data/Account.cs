using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SEproject.Data
{
    class Account
    {
        string token;
        string tenant;
        double expire_date;

        public int Login(string id, string pw)
        {
            HttpClient client = new HttpClient();
            var uri = new Uri("http://nekop.kr:3000/api/v1/request_auth");
            KeyValuePair<string, string>[] pair = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("username",id),
                new KeyValuePair<string, string>("password",pw)
            };

            var form = new FormUrlEncodedContent(pair);
            try
            {
                var response = client.PostAsync(uri, form).Result;
                var result = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (result["code"].ToString() == "0")
                {
                    token = result["msg"]["token"].ToString();
                    tenant = result["msg"]["tenant"].ToString();
                    expire_date = Double.Parse(result["msg"]["expire_date"].ToString());

                    return 0;
                }
                return -1;
            }
            catch (System.AggregateException aex)
            {
                return -2;
            }
            catch(Exception ex)
            {
                return -2;
            }
        }

        public string getToken()
        {
            return token;
        }
    }
}
