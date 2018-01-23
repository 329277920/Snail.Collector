using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Security
{
    /// <summary>
    /// 提供对数据的hash操作
    /// </summary>
    public class Hash
    {
        public static byte[] MD5(byte[] data)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                return md5.ComputeHash(data);
            }
        }

        public static byte[] SHA128(byte[] data)
        {
            using (var sha = new SHA1CryptoServiceProvider())
            {
                return sha.ComputeHash(data);
            }
        }      

        public static byte[] SHA256(byte[] data)
        {
            using (var sha = new SHA256CryptoServiceProvider())
            {
                return sha.ComputeHash(data);
            }
        }

        public static byte[] SHA384(byte[] data)
        {
            using (var sha = new SHA384CryptoServiceProvider())
            {
                return sha.ComputeHash(data);
            }
        }

        public static byte[] SHA512(byte[] data)
        {
            using (var sha = new SHA512CryptoServiceProvider())
            {
                return sha.ComputeHash(data);
            }
        }
        
        public static string ByteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr.ToLower();
        }
    }
}
