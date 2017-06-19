using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApiToken.Common
{
    public static class DESEncryptor
    {
        /// <summary>
        /// 加密16位
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Encrypt16(string pToEncrypt, string sKey)
        {
            try
            {
                byte[] keyIV = new byte[8];
                byte[] keyBytes = new byte[8];
                byte[] keysArray = Convert.FromBase64String(sKey);
                byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
                for (int i = 0; i < 8; i++)
                {
                    keyIV[i] = keysArray[i];
                    keyBytes[i] = keysArray[i];
                }
                DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();
                // java 默认的是ECB模式，PKCS5padding；c#默认的CBC模式，PKCS7padding 所以这里我们默认使用ECB方式
                desProvider.Mode = CipherMode.ECB;
                desProvider.Padding = PaddingMode.PKCS7;
                MemoryStream memStream = new MemoryStream();
                CryptoStream crypStream = new CryptoStream(memStream, desProvider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                crypStream.Write(inputByteArray, 0, inputByteArray.Length);
                crypStream.FlushFinalBlock();

                return Convert.ToBase64String(memStream.ToArray());
            }
            catch
            {
                return pToEncrypt;
            }
        }
        //16位解密方法  
        public static string Decrypt16(string pToDecrypt, string sKey)
        {
            pToDecrypt = pToDecrypt.Replace(" ", "+");
            //byte[] keyBytes = Encoding.UTF8.GetBytes(sKey);
            //byte[] keyIV = keyBytes;
            byte[] keyIV = new byte[8];
            byte[] keyBytes = new byte[8];
            byte[] keysArray = Convert.FromBase64String(sKey);
            byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
            for (int i = 0; i < 8; i++)
            {
                keyIV[i] = keysArray[i];
                keyBytes[i] = keysArray[i];
            }
            DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();
            // java 默认的是ECB模式，PKCS5padding；c#默认的CBC模式，PKCS7padding 所以这里我们默认使用ECB方式
            desProvider.Mode = CipherMode.ECB;
            desProvider.Padding = PaddingMode.PKCS7;
            MemoryStream memStream = new MemoryStream();
            CryptoStream crypStream = new CryptoStream(memStream, desProvider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            crypStream.Write(inputByteArray, 0, inputByteArray.Length);
            crypStream.FlushFinalBlock();

            return Encoding.UTF8.GetString(memStream.ToArray());
        }
    }
    /// <summary>
    /// MD5
    /// </summary>
    public static class MD5Encryptor
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <returns></returns>
        public static string Encrypt(string pToEncrypt)
        {
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] fromData = System.Text.Encoding.Unicode.GetBytes(pToEncrypt);
                byte[] targetData = md5.ComputeHash(fromData);
                string byte2String = null;
                for (int i = 0; i < targetData.Length; i++)
                {
                    byte2String += targetData[i].ToString("x");
                }

                return byte2String;
            }
            catch
            {
                return pToEncrypt;
            }
        }   
    }
}