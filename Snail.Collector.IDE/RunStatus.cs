using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.IDE
{
    /// <summary>
    /// 执行状态
    /// </summary>
    public class RunStatus
    {
        /// <summary>
        /// 当前用户数
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 总请求数
        /// </summary>
        public int TotalReqCount { get; set; }

        /// <summary>
        /// 总返回数
        /// </summary>
        public int TotalResCount { get; set; }

        /// <summary>
        /// 失败请求数
        /// </summary>
        public int ErrorReqCount { get; set; }

        /// <summary>
        /// 成功请求数
        /// </summary>
        public int SuccessReqCount { get; set; }

        /// <summary>
        /// 获取或设置当前是否正在执行
        /// </summary>
        public bool Running { get; set; }

        public void Init()
        {
            this.UserCount = 0;
            this.TotalReqCount = 0;
            this.TotalResCount = 0;
            this.ErrorReqCount = 0;
            this.SuccessReqCount = 0;
            this.Running = false;
        }
    }
}
