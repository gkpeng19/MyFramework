using G.Util.Redis;
using G.Util.Tool;
using GOMFrameWork;
using GOMFrameWork.DataEntity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace G.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            OAuthClientTest test = new OAuthClientTest();
            var t = test.Get_Accesss_Token_By_Resource_Owner_Password_Credentials_Grant();
            Console.ReadKey();
        }
    }

    public class OAuthClientTest
    {
        private HttpClient _httpClient;

        public OAuthClientTest()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:50185");
        }

        public async Task Get_Accesss_Token_By_Resource_Owner_Password_Credentials_Grant()
        {
            var token = await GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Console.WriteLine(await (await _httpClient.GetAsync("/api/Test/GetName")).Content.ReadAsStringAsync());
        }

        async Task<string> GetAccessToken()
        {
            var clientId = "1234";
            var clientSecret = "5678";

            var parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", "password");
            parameters.Add("username", "gkpeng");
            parameters.Add("password", "123456");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(clientId + ":" + clientSecret))
                );

            var response = await _httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters));
            var responseValue = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JObject.Parse(responseValue)["access_token"].Value<string>();
            }
            else
            {
                Console.WriteLine(responseValue);
                return string.Empty;
            }
        }
    }

    public class Park : GOMFrameWork.DataEntity.EntityBase
    {
        public Park()
        {
            base.TableName = "tab_registerpark";
            base.PrimaryKey = "iid";
        }

        protected override bool IsNullUIValue(string key, object value)
        {
            string v = value.ToString();
            if (v.Length == 0)
            {
                return true;
            }
            return false;
        }

        public override void SetUIValue(string key, object value)
        {
            var newvalue = value.ToString();
            var item = Collection[key];
            if (item == null)
            {
                if (!IsNullUIValue(key, newvalue))
                {
                    Collection[key] = new EntityItem() { Key = key, Value = newvalue };
                }
            }
            else
            {
                if (IsNullUIValue(key, newvalue))
                {
                    if (ID > 0)
                    {
                        //清空数据库字段中的值
                        (item as EntityItem).Value = DBNull.Value;
                    }
                    else
                    {
                        Collection.Remove(key);
                    }
                }
                else
                {
                    (item as EntityItem).Value = newvalue;
                }
            }
        }
    }
}
