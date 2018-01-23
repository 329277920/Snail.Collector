using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Snail.Security
{
    /// <summary>
    /// 封装MD5散列的相关方法
    /// </summary>
    public sealed class MD5
    {
        /// <summary>
        /// 计算任意字节数组的Hash值
        /// </summary>
        /// <param name="buffer">输入字节数组</param>
        /// <returns>返回128位Hash值</returns>
        public static byte[] ComputeHash(Byte[] buffer)
        {
            return new MD5CryptoServiceProvider().ComputeHash(buffer);
        }

        /// <summary>
        /// 计算任意字节数组的Hash值
        /// </summary>
        /// <param name="data">输入字节数组</param>
        /// <returns>返回128位Hash值，并将所有字节转化为16进制值，32字符</returns>
        public static string ComputeHashToHex(Byte[] buffer)
        {
            return ConvertToHex(ComputeHash(buffer));
        }

        /// <summary>
        /// 计算任意字符串的Hash值
        /// </summary>
        /// <param name="data">输入字符串</param>
        /// <param name="encode">转字节数组编码方式</param>
        /// <returns>返回128位Hash值</returns>
        public static byte[] ComputeHash(string data,Encoding encode)
        {
            return ComputeHash((encode.GetBytes(data)));
        }

        /// <summary>
        /// 计算任意字符串的Hash值
        /// </summary>
        /// <param name="data">输入字符串</param>
        /// <param name="encode">转字节数组编码方式</param>
        /// <returns>返回128位Hash值，并将所有字节转化为16进制值，32字符</returns>
        public static string ComputeHashToHex(string data, Encoding encode)
        {
            return ComputeHashToHex(encode.GetBytes(data));
        }

        /// <summary>
        /// 计算任意字节序列的Hash值
        /// </summary>
        /// <param name="inputStream">输入字节流</param>
        /// <returns>返回128位Hash值</returns>
        public static byte[] ComputeHash(Stream inputStream)
        {
            return new MD5CryptoServiceProvider().ComputeHash(inputStream);
        }

        /// <summary>
        /// 计算任意字节序列的Hash值
        /// </summary>
        /// <param name="inputStream">输入字节流</param>
        /// <returns>返回128位Hash值，并将所有字节转化为16进制值，32字符</returns>
        public static string ComputeHashToHex(Stream inputStream)
        {
            return ConvertToHex(ComputeHash(inputStream));
        }

        /// <summary>
        /// 将计算出的hash值转化成16进制，32字符
        /// </summary>
        /// <param name="hashData">Hash值</param>
        /// <returns>返回16进制字符串</returns>
        public static string ConvertToHex(byte[] hashData)
        {
            StringBuilder sBuilder = new StringBuilder(32);

            for (int i = 0; i < hashData.Length; i++)
            {
                sBuilder.Append(hashData[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
