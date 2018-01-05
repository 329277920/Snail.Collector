using System.IO;

namespace Snail.Configuration
{
    /// <summary>
    /// 配置文件接口类
    /// </summary>
    public interface IConfiguration
    {
        void Fill(Stream stream);
    }
}
