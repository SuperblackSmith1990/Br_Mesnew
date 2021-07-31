using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Modules
{
    public class ProcessEntity
    {
        /// <summary>
        /// 进程Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 进程名
        /// </summary>
        public string ProcessName { get; set; }
        /// <summary>
        /// 占用内存
        /// </summary>
        public string TotalMemory { get; set; }
        /// <summary>
        /// 启动时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FileName { get; set; }
    }
}
