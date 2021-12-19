using System.Data;

namespace JHRS.CoreWebPrint.Entities
{
    /// <summary>
    /// 打印数据实体
    /// </summary>
    public class PrintDataEntity<T>
    {
        /// <summary>
        /// 报表数据源，由服务器端返回
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 待打印数据使用的报表，由服务器端返回告诉客户端控件
        /// </summary>
        public string FrxFile { get; set; }
    }
}