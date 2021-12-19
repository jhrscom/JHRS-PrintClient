using JHRS.PrintClient.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JHRS.PrintClient.Extensions
{
    /// <summary>
    /// xml辅助类
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// 将对象序列化为xml文件
        /// </summary>
        /// <param name="data"></param>
        public static void SerializeToXmlFile(object data)
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer xz = new XmlSerializer(data.GetType());
                xz.Serialize(sw, data);
                var xml = sw.ToString();

                File.WriteAllText(Constants.ConfigSavePath, xml);
            }
        }

        /// <summary>
        /// 将xml文件反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlFilePath"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlFilePath)
        {
            if (!File.Exists(xmlFilePath)) return default;
            FileStream fs = File.Open(xmlFilePath, FileMode.Open);
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                XmlSerializer xz = new XmlSerializer(typeof(T));
                T t = (T)xz.Deserialize(sr);
                return t;
            }
        }
    }
}
