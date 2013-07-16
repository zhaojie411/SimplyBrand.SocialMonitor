using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SimplyBrand.SocialMonitor.Business.Utility
{
    public static class JsonHelper
    {
        public static string ToJson(this Object source)
        {
            return ToJson(source, source.GetType());
        }

        public static string ToJson(Object source, Type type)
        {
            DataContractJsonSerializer serilializer = new DataContractJsonSerializer(type);
            using (Stream stream = new MemoryStream())
            {
                serilializer.WriteObject(stream, source);
                stream.Flush();
                stream.Position = 0;
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        public static T ParseJSON<T>(this string str)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(str)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
