using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JHRS.WebFormWebPrint.Extensions
{
    /// <summary>
    /// Json辅助扩展操作
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// 将对象转换为JSON字符串
        /// </summary> 
        /// <param name="obj">要转换的对象</param>
        /// <param name="camelCase">是否小写名称</param>
        /// <param name="indented"></param>
        /// <returns></returns>
        public static string ToJson(this object obj, bool camelCase = false, bool indented = false)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            if (camelCase)
            {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
            if (indented)
            {
                settings.Formatting = Formatting.Indented;
            }
            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}