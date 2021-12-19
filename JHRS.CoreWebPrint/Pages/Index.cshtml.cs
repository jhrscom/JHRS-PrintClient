using JHRS.CoreWebPrint.Entities;
using JHRS.CoreWebPrint.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;

namespace JHRS.CoreWebPrint.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        /// <summary>
        /// 下载打印客户端
        /// </summary>
        /// <returns></returns>
        public FileResult OnGetDownload()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "wwwroot/print/setup.zip");
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/octet-stream", "setup.zip");
        }

        /// <summary>
        /// 获取打印数据
        /// </summary>
        /// <returns></returns>
        public ContentResult OnGetPrintData()
        {
            if ("https://jhrs.com".Equals(Request.Query["par1"].ToString())&& "znlive.com".Equals(Request.Query["myblog"].ToString()))
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
                        二维码 = "https://znlive.com/the-best-cheap-vpn",
                        生产厂家 = "jhrs.com 科技有限公司",
                        规格 = "GGX-1" + i,
                        设备名称 = "X射線髮射器",
                        责任人 = "趙佳發",
                        质保日期 = DateTime.Now.AddYears(i + 1)
                    });
                }

                var print = new PrintDataEntity<List<SbEntity>> { Data = list };

                var path = Path.Combine(Environment.CurrentDirectory, "wwwroot/print/设备标牌.frx");

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
                        return Content(new { Success = false, ErrorMsg = ex.Message }.ToJson());
                    }
                }
                return Content(print.ToJson());
            }
            return Content(new { Success = false, ErrorMsg = "狗日的，参数不对，重新传！" }.ToJson());
        }
    }
}