using Newtonsoft.Json;

namespace SharpNET.Utilities
{
    /// <summary>
    /// Shortcut methods for serializing objects to and from Xml
    /// </summary>
    public static class JsonSerializationExtensions
    {
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
