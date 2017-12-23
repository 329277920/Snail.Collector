using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Collector.JSAdapter
{
    /// <summary>
    /// 异步方法回调参数
    /// </summary>
    public class JSCallBackEventArgs
    {
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 获取或设置异常信息
        /// </summary>
        public JSException error { get; set; }

        /// <summary>
        /// 回调返回对象
        /// </summary>
        public object data { get; set; }
    }
}
