using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JHRS.PrintClient.Extensions
{
    /// <summary>
    /// Http请求辅助类，用于从打印接口获取打印数据
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        /// 获取打印数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">打印接口</param>
        /// <returns></returns>
        public static T Get<T>(string url)
        {
            url = HttpUtility.UrlDecode(url);
            if (!url.Contains("?")) throw new Exception("未指定有效的打印接口参数，必须包含有查询参数（?）！");

            var baseAddress = new Uri(url.Substring(0, url.IndexOf('?')));

            var pars = url.Substring(url.IndexOf('?') + 1);
            var arrary = pars.Split('&');
            List<KeyValuePair<string, string>> postList = new List<KeyValuePair<string, string>>();

            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })

            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                foreach (var item in arrary)
                {
                    var items = item.Split('=');
                    if (items[0].ToLower() == "cookie".ToLower())
                    {
                        var cookies = items[1].Split('|');
                        foreach (var cookie in cookies)
                        {
                            var c = cookie.Split(',');
                            cookieContainer.Add(baseAddress, new Cookie(c[0], c[1]));
                        }
                    }
                    else
                    {
                        postList.Add(new KeyValuePair<string, string>(items[0], items[1]));
                    }
                }

                var content = new FormUrlEncodedContent(postList.ToArray());

                HttpResponseMessage response = client.PostAsync(url.Substring(0, url.IndexOf('?')), content).Result;
                response.EnsureSuccessStatusCode();

                var data = response.Content.ReadAsStringAsync().Result;
                LogHelper.WriteLog($"获取到的原始打印数据：{data}");
                return JsonConvert.DeserializeObject<T>(data);
            }
        }
    }
}
