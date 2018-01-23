using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Security.Cryptography;

namespace Snail.Security
{
    /// <summary>
    /// 提供对RSA加密解密的封装
    /// </summary>
    public class RSA
    {
        #region PEM 格式密钥转换

        /// <summary>
        /// 将pem格式私钥转换成.net支持的xml格式私钥
        /// </summary>
        /// <param name="privateKey">pem私钥</param>
        /// <returns>返回xml格式私钥</returns>
        public static string ConvertToXmlPrivateKey(string privateKey)
        {
            var bytes = Convert.FromBase64String(privateKey);
            var pkParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(bytes);
            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                Convert.ToBase64String(pkParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(pkParam.PublicExponent.ToByteArrayUnsigned()),
                Convert.ToBase64String(pkParam.P.ToByteArrayUnsigned()),
                Convert.ToBase64String(pkParam.Q.ToByteArrayUnsigned()),
                Convert.ToBase64String(pkParam.DP.ToByteArrayUnsigned()),
                Convert.ToBase64String(pkParam.DQ.ToByteArrayUnsigned()),
                Convert.ToBase64String(pkParam.QInv.ToByteArrayUnsigned()),
                Convert.ToBase64String(pkParam.Exponent.ToByteArrayUnsigned()));
        }

        /// <summary>
        /// 将pem格式公钥转换成.net支持的xml格式公钥
        /// </summary>
        /// <param name="publicKey">pem公钥</param>
        /// <returns>返回xml格式公钥</returns>
        public static string ConvertToXmlPublicKey(string publicKey)
        {
            var bytes = Convert.FromBase64String(publicKey);
            var pkParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(bytes);
            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
                Convert.ToBase64String(pkParam.Modulus.ToByteArrayUnsigned()),
                Convert.ToBase64String(pkParam.Exponent.ToByteArrayUnsigned()));
        }

        #endregion

        #region 私钥加密 - 公钥解密

        /// <summary>
        /// 用私钥加密
        /// </summary>
        /// <param name="data">被加密数据</param>
        /// <param name="xmlPrivateKey">xml格式私钥</param>        
        /// <param name="algorithm">加密参数，与解密参数需一致，如："RSA/ECB/PKCS1Padding"</param>
        /// <returns>返回加密字节的base64格式</returns>
        public static byte[] EncryptByPrivateKey(byte[] data, string xmlPrivateKey, string algorithm = "RSA/ECB/PKCS1Padding")
        {
            //加载私钥  
            RSACryptoServiceProvider privateRsa = new RSACryptoServiceProvider();
            privateRsa.FromXmlString(xmlPrivateKey);
            //转换密钥  
            AsymmetricCipherKeyPair keyPair = DotNetUtilities.GetKeyPair(privateRsa);
            IBufferedCipher c = CipherUtilities.GetCipher(algorithm);
            // 第一个参数为true表示加密，为false表示解密；第二个参数表示密钥 
            c.Init(true, keyPair.Private);             
            return c.DoFinal(data);           
        }

        /// <summary>
        /// 用公钥解密
        /// </summary>
        /// <param name="data">待解密数据</param>
        /// <param name="xmlPublicKey">xml格式公钥</param>
        /// <param name="algorithm">解密参数，与加密参数需一致，如："RSA/ECB/PKCS1Padding"</param>
        /// <returns></returns>
        public static byte[] DecryptByPublicKey(byte[] encryptdata, string xmlPublicKey, string algorithm)
        {
            RSACryptoServiceProvider publicRsa = new RSACryptoServiceProvider();
            publicRsa.FromXmlString(xmlPublicKey);

            AsymmetricKeyParameter keyPair = DotNetUtilities.GetRsaPublicKey(publicRsa);
            //转换密钥  
            // AsymmetricCipherKeyPair keyPair = DotNetUtilities.GetRsaKeyPair(publicRsa);
            IBufferedCipher c = CipherUtilities.GetCipher(algorithm);
            //第一个参数为true表示加密，为false表示解密；第二个参数表示密钥             
            c.Init(false, keyPair);
            byte[] outBytes = c.DoFinal(encryptdata);//解密  
            return outBytes;
        }

        #endregion

        #region 公钥加密 - 私钥解密

        /// <summary>  
        /// 公钥加密
        /// </summary>  
        /// <param name="data">待加密的数据</param>  
        /// <param name="xmlPublicKey">待加密的字节数组</param>  
        /// <returns>返回加密的字节数组</returns>  
        public static byte[] EncryptByPublicKey(byte[] data, string xmlPublicKey)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            return rsa.Encrypt(data, false);
        }

        /// <summary>  
        /// 私钥解密
        /// </summary>  
        /// <param name="encryptdata">已加密的字节数组</param>  
        /// <param name="xmlPublicKey">待加密的字节数组</param>  
        /// <returns>返回加密的字节数组</returns>  
        public static byte[] DecryptByPrivateKey(byte[] encryptdata, string xmlPrivateKey)
        {          
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            return rsa.Decrypt(encryptdata, false); 
        }

        #endregion

        #region 私钥签名 - 公钥验签

        /// <summary>
        /// 私钥签名
        /// </summary>
        /// <param name="xmlPrivateKey">xml格式私钥</param>
        /// <param name="rgbHash">需签名的hash数据</param>
        /// <param name="hashType">指定hash的类型（MD5|SHA1）</param>
        /// <returns>签名后的值</returns>
        public static byte[] SignatureFormatter(string xmlPrivateKey, byte[] rgbHash, string hashType)
        {            
            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.FromXmlString(xmlPrivateKey);
            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
            formatter.SetHashAlgorithm(hashType);
            return formatter.CreateSignature(rgbHash);
        }

        /// <summary>
        /// 公钥验签
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="rgbHash">待签名数据的hash值</param>
        /// <param name="signData">签名的值</param>
        /// <param name="hashType">指定hash的类型（MD5|SHA1）</param>
        /// <returns>签名是否符合</returns>
        public static bool SignatureDeformatter(string xmlPublicKey, byte[] rgbHash, byte[] signData, string hashType)
        {
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
            RSA.FromXmlString(xmlPublicKey);
            RSAPKCS1SignatureDeformatter RSADeformatter = new RSAPKCS1SignatureDeformatter(RSA);           
            RSADeformatter.SetHashAlgorithm(hashType);
            return RSADeformatter.VerifySignature(rgbHash, signData);
        }
        #endregion       
    }
}
