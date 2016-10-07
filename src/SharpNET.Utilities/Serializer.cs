using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace SharpNET.Utilities
{
    /// <summary>
    /// Shortcut methods for serializing objects to and from Xml
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// Load an object from an xml string
        /// </summary>
        /// <param name="FileName">Xml file name</param>
        /// <returns>The object created from the xml file</returns>
        public static T FromXml<T>(string objectXml) where T : class
        {
            using (var stream = new StringReader(objectXml))
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(stream) as T;
            }
        }

        /// <summary>
        /// Save an object to an xml string
        /// </summary>
        /// <param name="objectInstance"></param>
        /// <returns></returns>
        public static string ToXml<T>(T obj)
        {
            var serializer = new XmlSerializer(obj.GetType());
            var sb = new StringBuilder();

            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, obj);
            }

            return sb.ToString();
        }

        public static T XmlDeserialize<T>(this string source) where T : class
        {
            return FromXml<T>(source);
        }

        public static string XmlSerialize(this object source)
        {
            return ToXml(source);
        }

        public static string ToJson(this object source)
        {
            return JsonConvert.SerializeObject(source);
        }

        public static T FromJson<T>(this string source)
        {
            return JsonConvert.DeserializeObject<T>(source);
        }
    }
}
