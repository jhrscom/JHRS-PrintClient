using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHRS.PrintClient.Entity
{
    /// <summary>
    /// 打印设置项
    /// </summary>
    public class Configs
    {
        /// <summary>
        /// 设置名称
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// 打印机名称
        /// </summary>
        public string DefaultPrinter { get; set; }

        /// <summary>
        /// 是否直接打印
        /// </summary>
        public bool DirectPrint { get; set; } = false;

        /// <summary>
        /// 当前选中的打印设置
        /// </summary>
        public bool Selected { get; set; } = false;

        /// <summary>
        /// 设置顺序
        /// </summary>
        public int Index { get; set; } = 1;
    }
}
