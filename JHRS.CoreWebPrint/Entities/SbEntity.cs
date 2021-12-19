namespace JHRS.CoreWebPrint.Entities
{
    /// <summary>
    /// 打印设备标签实体信息，保存从数据库里面查询出来的数据
    /// </summary>
    public class SbEntity
    {
        public string 标题 { get; set; }
        public string 序号 { get; set; }
        public string 设备名称 { get; set; }
        public string 规格 { get; set; }
        public string 型号 { get; set; }
        public DateTime 启用日期 { get; set; }
        public string 序列号 { get; set; }
        public string 生产厂家 { get; set; }
        public string 条码 { get; set; }
        public string 二维码 { get; set; }
        public string 责任人 { get; set; }
        public string 使用科室 { get; set; }
        public DateTime 质保日期 { get; set; }
    }
}
