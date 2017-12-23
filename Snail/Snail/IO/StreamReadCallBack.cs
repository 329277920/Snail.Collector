using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.IO
{

    /// <summary>
    /// 读取流回调委托类型
    /// </summary>
    /// <param name="buffer">读取字节缓冲区</param>   
    /// <param name="count">读取字节数</param>
    /// <returns>返回是否继续读取</returns>
    public delegate bool StreamReadCallBack(byte[] buffer, int count);
}
