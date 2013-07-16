using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.Utility
{
    public class EncryHelper
    {
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="pToEncrypt">待加密的字符串</param>
        /// <returns></returns>
        public static string MD5Encrypt(string pToEncrypt)
        {
            string pwd = "";

            MD5 md5 = MD5.Create();
            // 加密后是一个字节类型的数组
            byte[] s = md5.ComputeHash(Encoding.Unicode.GetBytes(pToEncrypt));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString("x2");
            }
            return pwd;

            //string md5Pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pToEncrypt, "MD5");
            //return md5Pwd;
        }

        /// <summary>
        /// DES 对字符串进行加密
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <returns></returns>
        public static string DESEncry(string pToEncrypt)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                //把字符串放到byte数组中  
                byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

                //建立加密对象的密钥和偏移量  
                //原文使用ASCIIEncoding.ASCII方法的GetBytes方法  
                //使得输入密码必须输入英文文本  
                des.Key = ASCIIEncoding.ASCII.GetBytes(DESKey);

                des.IV = ASCIIEncoding.ASCII.GetBytes(DESIV);

                StringBuilder ret = new StringBuilder();

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);

                        cs.FlushFinalBlock();

                        foreach (byte b in ms.ToArray())
                        {
                            //Format  as  hex  
                            ret.AppendFormat("{0:X2}", b);
                        }
                    }
                }

                return ret.ToString();
            }
            catch
            {
                return "";
            }

        }
        /// <summary>
        /// DES 对字符串进行解密
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <returns></returns>
        public static string DESDecry(string pToDecrypt)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                Byte[] inputByteArray = new byte[pToDecrypt.Length / 2];

                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));

                    inputByteArray[x] = (byte)i;
                }
                //建立加密对象的密钥和偏移量，此值重要，不能修改 
                des.Key = ASCIIEncoding.ASCII.GetBytes(DESKey);

                des.IV = ASCIIEncoding.ASCII.GetBytes(DESIV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);

                        cs.FlushFinalBlock();

                        return System.Text.Encoding.Default.GetString(ms.ToArray());
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 对文件内容进行DES加密
        /// </summary>
        /// <param name="sourceFile">待加密的文件绝对路径</param>
        /// <param name="destFile">加密后的文件保存的绝对路径</param>
        public static void EncryptFile(string sourceFile, string destFile)
        {
            if (!File.Exists(sourceFile))
                throw new FileNotFoundException("指定的文件路径不存在！", sourceFile);

            byte[] btKey = Encoding.Default.GetBytes(DESKey);

            byte[] btIV = Encoding.Default.GetBytes(DESIV);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] btFile = File.ReadAllBytes(sourceFile);

            using (FileStream fs = new FileStream(destFile, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    using (CryptoStream cs = new CryptoStream(fs, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(btFile, 0, btFile.Length);
                        cs.FlushFinalBlock();
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 对文件内容进行DES加密，加密后覆盖掉原来的文件
        /// </summary>
        /// <param name="sourceFile">待加密的文件的绝对路径</param>
        public static void EncryptFile(string sourceFile)
        {
            EncryptFile(sourceFile, sourceFile);
        }

        /// <summary>
        /// 对文件内容进行DES解密
        /// </summary>
        /// <param name="sourceFile">待解密的文件绝对路径</param>
        /// <param name="destFile">解密后的文件保存的绝对路径</param>
        public static void DecryptFile(string sourceFile, string destFile)
        {
            if (!File.Exists(sourceFile)) throw new FileNotFoundException("指定的文件路径不存在！", sourceFile);

            byte[] btKey = Encoding.Default.GetBytes(DESKey);

            byte[] btIV = Encoding.Default.GetBytes(DESIV);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] btFile = File.ReadAllBytes(sourceFile);

            using (FileStream fs = new FileStream(destFile, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    using (CryptoStream cs = new CryptoStream(fs, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(btFile, 0, btFile.Length);
                        cs.FlushFinalBlock();
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 对文件内容进行DES解密，加密后覆盖掉原来的文件
        /// </summary>
        /// <param name="sourceFile">待解密的文件的绝对路径</param>
        public static void DecryptFile(string sourceFile)
        {
            DecryptFile(sourceFile, sourceFile);
        }

        /// <summary>
        /// DES加密的Key
        /// </summary>
        public static string DESKey
        {
            get
            {

                return ConfigurationManager.AppSettings["EncryKey"].ToString();
            }
        }

        /// <summary>
        /// DES加密的IV
        /// </summary>
        public static string DESIV
        {
            get
            {
                return ConfigurationManager.AppSettings["EncryIV"].ToString();
            }
        }

        /// <summary>
        /// 将字符使用Base64加密
        /// </summary>
        /// <param name="source">待加密的字符</param>
        /// <returns></returns>
        public static string EncodingForString(string source)
        {
            return EncodingForString(source, System.Text.Encoding.GetEncoding(54936));
        }

        /// <summary>
        /// 将字符使用Base64加密
        /// </summary>
        /// <param name="source">待加密的字符</param>
        /// <param name="encode">字符编码</param>
        /// <returns></returns>
        public static string EncodingForString(string source, Encoding encode)
        {
            return Convert.ToBase64String(encode.GetBytes(source));
        }

        /// <summary>
        /// 解密base 64字符
        /// </summary>
        /// <param name="base64String">待解密的base 64位字符</param>
        /// <returns></returns>
        public static string DecodingForString(string base64String)
        {
            return DecodingForString(base64String, System.Text.Encoding.GetEncoding(54936));
        }


        public static string DecodeFrom64(string encodedData)
        {

            byte[] encodedDataAsBytes

                = System.Convert.FromBase64String(encodedData);

            string returnValue =

               System.Text.Encoding.Unicode.GetString(encodedDataAsBytes);

            return returnValue;

        }

        /// <summary>
        /// 解密base 64字符
        /// </summary>
        /// <param name="base64String">待解密的base 64位字符</param>
        /// <param name="encode">字符编码</param>
        /// <returns></returns>
        public static string DecodingForString(string base64String, Encoding encode)
        {
            return encode.GetString(Convert.FromBase64String(base64String));
        }

        /// <summary>
        /// 对任意类型的文件进行base64加码
        /// </summary>
        /// <param name="fileName">文件的路径和文件名</param>
        /// <returns>对文件进行base64编码后的字符串</returns>
        ///  <returns></returns>
        public static string EncodingForFile(string fileName)
        {
            using (FileStream fs = File.OpenRead(fileName))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    return Convert.ToBase64String(br.ReadBytes((int)fs.Length));
                }
            }
        }

        /// <summary>
        /// 把经过base64编码的字符串保存为文件
        /// </summary>
        /// <param name="base64String">经base64加码后的字符串</param>
        /// <param name="fileName">保存文件的路径和文件名</param>
        /// <returns>保存文件是否成功</returns>
        public static bool SaveDecodingToFile(string base64String, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, System.IO.FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(Convert.FromBase64String(base64String));

                    return true;
                }
            }
        }

        /// <summary>
        /// 从网络地址一取得文件并转化为base64编码
        /// </summary>
        /// <param name="url">文件的url地址,一个绝对的url地址</param>
        /// <param name="objWebClient">System.Net.WebClient 对象</param>
        /// <returns></returns>
        public static string EncodingFileFromUrl(string url, System.Net.WebClient objWebClient)
        {
            return Convert.ToBase64String(objWebClient.DownloadData(url));
        }

        /// <summary>
        /// 从网络地址一取得文件并转化为base64编码
        /// </summary>
        /// <param name="url">文件的url地址,一个绝对的url地址</param>
        /// <returns>将文件转化后的base64字符串</returns>
        public static string EncodingFileFromUrl(string url)
        {
            return EncodingFileFromUrl(url, new System.Net.WebClient());
        }

        /// <summary>
        /// 解密当前Cookie中的用户信息
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string DencryCurrentInfo(string source)
        {
            return DecodingForString(DESDecry(source));
        }

        /// <summary>
        /// 加密保存到Cookie中的字符
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string EncryCurrentInfo(string source)
        {
            return DESEncry(EncodingForString(source));
        }

        /// <summary>
        /// 将sina的tokenstring转成json数据
        /// </summary>
        /// <param name="signedRequest"></param>
        /// <param name="appkey"></param>
        /// <returns></returns>
        public static string SinaTokenConvertString(string signedRequest, string appkey)
        {
            string[] parameters = signedRequest.Split('.');
            var encodedSig = parameters[0];
            var payload = parameters[1];
            var sha256 = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(appkey));
            var expectedSig = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(payload)));
            sha256.Clear();
            encodedSig = parameters[0].Length % 4 == 0 ? parameters[0] : parameters[0].PadRight(parameters[0].Length + (4 - parameters[0].Length % 4), '=').Replace("-", "+").Replace("_", "/");
            payload = parameters[1].Length % 4 == 0 ? parameters[1] : parameters[1].PadRight(parameters[1].Length + (4 - parameters[1].Length % 4), '=').Replace("-", "+").Replace("_", "/");
            string json = Encoding.UTF8.GetString(Convert.FromBase64String(payload));
            return json;
        }
    }
}
