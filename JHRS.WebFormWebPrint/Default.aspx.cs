using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastReport.Utils;
using FastReport;
using JHRS.WebFormWebPrint.Entites;
using System.IO;
using JHRS.WebFormWebPrint.Extensions;

namespace JHRS.WebFormWebPrint
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ("Download".Equals(Request["action"]))
                {
                    DownloadEvent();
                    return;
                }
                string reportFile = Server.MapPath("/report/设备标牌.frx");
                //这里为了演示，直接new一个集合，您应该从另外一个项目将数据post过来再反序列化。通过Request.Form["参数名"]，将它反序列化即可。
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
                        二维码 = "https://znlive.com/the-best-cheap-vpn",
                        生产厂家 = "jhrs.com 科技有限公司",
                        规格 = "GGX-1" + i,
                        设备名称 = "X射線髮射器",
                        责任人 = "趙佳發",
                        质保日期 = DateTime.Now.AddYears(i + 1)
                    });
                }
                var dt = list.ToDataTable();

                WebReport1.RegisterData(dt, "打印数据源");
                WebReport1.Report.Load(reportFile);
                WebReport1.Prepare();
            }
        }

        /// <summary>
        /// 下载打印客户端程序
        /// </summary>
        private void DownloadEvent()
        {
            using (FileStream fs = new FileStream(Server.MapPath("/report/setup.zip"), FileMode.Open))
            {
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                string fileName = "jhrs.com打印客户端.zip";
                if (HttpContext.Current.Request.Browser.Browser == "IE" ||
                    HttpContext.Current.Request.Browser.Browser == "InternetExplorer")
                {
                    fileName = HttpUtility.UrlPathEncode(fileName);
                }
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
    }
}