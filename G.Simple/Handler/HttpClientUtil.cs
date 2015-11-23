using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace G.Simple
{
    public class HttpClientUtil
    {
        private static HttpClient _httpClient;

        static HttpClientUtil()
        {
            _httpClient = new HttpClient();
            var baseAddress = ConfigurationManager.AppSettings["ApiBaseAddress"];
            _httpClient.BaseAddress = new Uri(baseAddress);
        }

        public static byte[] DoRequest(HttpContext ctx)
        {
            try
            {
                var token = GetAccessToken();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                Task<HttpResponseMessage> response = null;
                if (ctx.Request.RequestType.Equals("get", StringComparison.CurrentCultureIgnoreCase))
                {
                    response = _httpClient.GetAsync(ctx.Request.RawUrl);
                }
                else
                {
                    var dic = new Dictionary<string, string>();
                    foreach (string key in ctx.Request.Form.Keys)
                    {
                        dic.Add(key, ctx.Request.Form[key]);
                    }
                    response = _httpClient.PostAsync(ctx.Request.RawUrl, new FormUrlEncodedContent(dic));
                }

                response.Wait();
                var responseValue = response.Result.Content.ReadAsByteArrayAsync();
                responseValue.Wait();
                return responseValue.Result;
            }
            catch
            {
                return null;
            }
        }

        static string GetAccessToken()
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

            var response = _httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters));
            response.Wait();
            var responseValue = response.Result.Content.ReadAsStringAsync();
            responseValue.Wait();
            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JObject.Parse(responseValue.Result)["access_token"].Value<string>();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}