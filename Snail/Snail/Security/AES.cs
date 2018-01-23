using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Security
{
    /// <summary>
    /// AES加密解密
    /// </summary>
    public class AES
    {
        /// <summary>
        /// 对指定字符串进行AES128加密
        /// </summary>
        /// <param name="decryptString">被加密的原字符</param>
        /// <param name="encryptKey">加密秘钥</param>
        /// <param name="encode">编码方式</param>
        /// <returns>返回加密后的值，并转换成Base64</returns>
        public static string EncryptToBase64(string decryptString, string encryptKey, Encoding encode)
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.Key = GetEncryptKey(encryptKey);
                aesProvider.Mode = CipherMode.ECB;
                aesProvider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor())
                {
                    byte[] buffer = encode.GetBytes(decryptString);
                    byte[] results = cryptoTransform.TransformFinalBlock(buffer, 0, buffer.Length);                   
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        /// <summary>
        /// 对指定字符串进行AES128解密
        /// </summary>
        /// <param name="encryptString">已经加密过并经过base64编码的值</param>
        /// <param name="encryptKey">加密秘钥</param>
        /// <param name="encode">编码方式</param>
        /// <returns>返回解密的，并经过encode指定编码方式解码后的值</returns>
        public static string DecryptFromBase64(string encryptString, string encryptKey, Encoding encode)
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.Key = GetEncryptKey(encryptKey);
                aesProvider.Mode = CipherMode.ECB;
                aesProvider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor())
                {
                    byte[] buffer = Convert.FromBase64String(encryptString);
                    byte[] results = cryptoTransform.TransformFinalBlock(buffer, 0, buffer.Length);
                    return encode.GetString(results);
                }
            }
        }

        private static byte[] GetEncryptKey(string encryptKey, int maxLen = 16)
        {
            var keyBytes = new byte[maxLen];             
            Encoding.ASCII.GetBytes(encryptKey).Take(maxLen).ToArray().CopyTo(keyBytes, 0);
            return keyBytes;
        }
    }
}
