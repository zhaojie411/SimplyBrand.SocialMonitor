using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SimplyBrand.SocialMonitor.Business.Utility
{
    public class EncodingUtility
    {
        public static string UnCodeUnicode(string str)
        {
            if (str != null)
            {
                string outStr = "";
                Regex reg = new Regex(@"(?i)\\u([0-9a-f]{4})");
                outStr = reg.Replace(str, delegate(Match m1)
                {
                    return ((char)Convert.ToInt32(m1.Groups[1].Value, 16)).ToString();
                });
                return outStr;
            }
            else
            {
                throw new ArgumentNullException("UnCodeUnicode function fail.str is NULL.");
            }

        }

        public static string UnCodeUnicodeX(string str)
        {
            if (str != null)
            {
                string outStr = "";
                Regex reg = new Regex(@"(?i)\\x([0-9a-f]{2})");
                outStr = reg.Replace(str, delegate(Match m1)
                {
                    return ((char)Convert.ToInt32(m1.Groups[1].Value, 16)).ToString();
                });
                return outStr;
            }
            else
            {
                throw new ArgumentNullException("UnCodeUnicodeX function fail.str is null");
            }

        }


        public static string ToUnicode(string str)
        {
            if (str != null)
            {
                byte[] bts = Encoding.Unicode.GetBytes(str);
                string r = "";
                for (int i = 0; i < bts.Length; i += 2) r += "\\u" + bts[i + 1].ToString("x").PadLeft(2, '0') + bts[i].ToString("x").PadLeft(2, '0');
                return r;
            }
            else
            {
                throw new ArgumentNullException("ToUnicode Function fail.str Is Null.");
            }

        }

        public static string Str2Hex(string strInput)
        {

            if (strInput != null)
            {
                System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();

                string unicodeString = strInput;//strinput就是需要转换的汉字字符串

                string str = "";

                Byte[] encodedBytes = utf8.GetBytes(unicodeString);//转换成数组

                for (int i = 0; i < (encodedBytes.Length); i++)
                {

                    str = str + "%25";//因为ReportingService中的特殊要求，需要在每个汉字前增加一个“%25，即%本身的编码”

                    str = str + System.Convert.ToString(encodedBytes[i], 16);//转换成16进制的编码。

                }

                return str;
            }
            else
            {
                throw new ArgumentNullException("Str2Hex function fail. strInput is Null.");
            }


        }

    }
}
