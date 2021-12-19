using JHRS.WebFormWebPrint.Entites;
using JHRS.WebFormWebPrint.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace JHRS.WebFormWebPrint
{
    public partial class print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ("PrintData".Equals(Request["action"]))
                {
                    PrintDataEvent();
                }
            }
        }

        /// <summary>
        /// 输出打印数据为json
        /// </summary>
        private void PrintDataEvent()
        {
            if ("znlive.com".Equals(Request["myblog"]) && "https://jhrs.com".Equals(Request.QueryString["par1"]))
            {
                List<SbEntity> list = new List<SbEntity>();
                for (int i = 0; i < 1; i++)
                {
                    list.Add(new SbEntity
                    {
                        标题 = "江湖人士醫院超聲設備標牌",
                        使用科室 = "超聲科-" + i,
                        启用日期 = DateTime.Now.AddDays(i),
                        型号 = "XH34534-" + i,
                        序列号 = "XLH-3452" + i,
                        序号 = (i + 1).ToString(),
                        条码 = "BH20190302002" + i,
                        二维码= "https://znlive.com/the-best-cheap-vpn",
                        生产厂家 = "jhrs.com 科技有限公司",
                        规格 = "GGX-1" + i,
                        设备名称 = "X射線髮射器",
                        责任人 = "趙佳發",
                        质保日期 = DateTime.Now.AddYears(i + 1)
                    });
                }

                var print = new PrintDataEntity<List<SbEntity>> { Data = list };

                string path = Server.MapPath("/report/设备标牌.frx");

                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        byte[] buffur = new byte[fs.Length];
                        fs.Read(buffur, 0, (int)fs.Length);
                        print.FrxFile = string.Join("^", buffur);
                    }
                    catch (Exception ex)
                    {
                        Response.Write(new { Success = false, ErrorMsg = ex.Message }.ToJson());
                    }
                }
                Response.Write(print.ToJson());
            }
        }
    }
}