using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.PrintClient.Entity
{
    /// <summary>
    /// 打印数据
    /// </summary>
    public class PrintData
    {
        /// <summary>
        /// 报表文件
        /// </summary>
        public string FrxFile { get; set; }

        /// <summary>
        /// 报表文件流
        /// </summary>
        public Stream Report
        {
            get
            {
                if (string.IsNullOrWhiteSpace(FrxFile)) return null;
                byte[] arrary = FrxFile.Split('^').Select(x => (byte)int.Parse(x)).ToArray();
                return new MemoryStream(arrary);
            }
        }

        /// <summary>
        /// 报表文件参数
        /// </summary>
        public Dictionary<string, object> Parameters { get; set; }

        /// <summary>
        /// 打印数据
        /// </summary>
        public Dictionary<string, DataTable> Data { get; set; }

        /// <summary>
        /// 打印数据源
        /// </summary>
        public DataSet DataSource
        {
            get
            {
                DataSet ds = new DataSet();
                foreach (KeyValuePair<string, DataTable> item in Data)
                {
                    item.Value.TableName = item.Key;
                    ds.Tables.Add(item.Value.Copy());
                }
                return ds;
            }
        }
    }
}
