using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.Utilities
{
    public static class EnumHelper<T>
    {
        private static Type _enumType;
        public static Type EnumType { get { return _enumType; } }

        private static Dictionary<string, EnumInfo> _enumStringValueMap;
        private static Dictionary<int, EnumInfo> _enumIntValueMap;

        public static IEnumerable<EnumInfo> InfoItems 
        {
            get
            {
                return _enumStringValueMap.Values;
            }
        }

        static EnumHelper()
        {
            _enumType = typeof(T);
            var values = Enum.GetValues(_enumType);

            _enumStringValueMap = new Dictionary<string, EnumInfo>(values.Length, StringComparer.InvariantCultureIgnoreCase);
            _enumIntValueMap = new Dictionary<int, EnumInfo>(values.Length);

            foreach (T value in values)
            {
                var info = new EnumInfo(value, _enumType);
                _enumStringValueMap.Add(value.ToString(), info);
                _enumIntValueMap.Add(info.NumericValue, info);
            }
        }

        public static IEnumerable<Enum> GetFlags(Enum input)
        {
            foreach (Enum value in Enum.GetValues(_enumType))
                if (input.HasFlag(value))
                    yield return value;
        }

        public static T Parse(string value)
        {
            if (string.IsNullOrEmpty(value) || !_enumStringValueMap.ContainsKey(value)) return _enumStringValueMap.Values.First().Value;
            return _enumStringValueMap[value].Value;
        }

        public static string GetName(int value)
        {
            return _enumIntValueMap[value].Name;
        }

        public static IEnumerable<string> GetNames()
        {
            return _enumStringValueMap.Values.Select(v => v.Name);
        }

        public static string GetDisplayValue(T value)
        {
            return _enumStringValueMap[value.ToString()].Display;
        }

        public static string GetDisplayValue(string value)
        {
            return _enumStringValueMap[value].Display; 
        }

        public static string GetDisplayValue(int value)
        {
            return _enumIntValueMap[value].Display;
        }

        public static IEnumerable<string> GetDisplayValues()
        {
            return _enumStringValueMap.Values.Select(v => v.Display);
        }

        public static EnumInfo GetInfo(T value)
        {
            return _enumStringValueMap[value.ToString()];
        }

        public static EnumInfo GetInfo(string value)
        {
            return _enumStringValueMap[value];
        }

        public static EnumInfo GetInfo(int value)
        {
            return _enumIntValueMap[value];
        }

        public class EnumInfo
        {
            public T Value { get; internal set; }
            public Enum EnumValue { get; internal set; }
            public string Name { get; internal set; }
            public int NumericValue { get; internal set; }
            public DisplayAttribute DisplayAttribute { get; internal set; }

            public string Display { get; private set; }
            //{
            //    get
            //    {
            //        if (DisplayAttribute == null || DisplayAttribute.Name == null) return Name;
            //        return DisplayAttribute.Name;
            //    }
            //}

            internal EnumInfo(T value, Type enumType)
            {
                var fieldInfo = enumType.GetField(value.ToString());
                Value = value;
                var rawValue = fieldInfo.GetValue(value);
                EnumValue = (Enum)rawValue;
                NumericValue = (int)rawValue;
                Name = value.ToString();
                DisplayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>(false);
                Display = Name;
                if (DisplayAttribute != null && DisplayAttribute.Name != null) Display = DisplayAttribute.Name;
            }
        }
    }


    //public abstract class EnumClassUtils<TClass> where TClass : class
    //{
        
    //    public static TEnum Parse<TEnum>(string value) where TEnum : struct, TClass
    //    {
    //        if (string.IsNullOrEmpty(value) || !_enumStringValueMap.ContainsKey(value)) return _enumStringValueMap.Values.First().Value;
    //        return _enumStringValueMap[value].Value;
    //    }

    //    public static string GetName(int value)
    //    {
    //        return _enumIntValueMap[value].Name;
    //    }

    //    public static IEnumerable<string> GetNames()
    //    {
    //        return _enumStringValueMap.Values.Select(v => v.Name);
    //    }

    //    public static string GetDisplayValue(T value)
    //    {
    //        return _enumStringValueMap[value.ToString()].Display;
    //    }

    //    public static string GetDisplayValue(string value)
    //    {
    //        return _enumStringValueMap[value].Display;
    //    }

    //    public static string GetDisplayValue(int value)
    //    {
    //        return _enumIntValueMap[value].Display;
    //    }

    //    public static IEnumerable<string> GetDisplayValues()
    //    {
    //        return _enumStringValueMap.Values.Select(v => v.Display);
    //    }
    //    public static EnumInfoCollection<TEnum> GetEnumInfoCollection<TEnum>() where TEnum : struct, TClass
    //    {
    //        return new EnumInfoCollection<TEnum>();
    //    }

    //    public class EnumInfoCollection<TEnum> where TEnum : struct, TClass
    //    {
    //        private static Type _enumType = typeof(TEnum);

    //        internal static Dictionary<string, EnumInfo> _enumStringValueMap;
    //        internal static Dictionary<int, EnumInfo> _enumIntValueMap;

    //        public EnumInfo this[string value]
    //        {
    //            get
    //            {
    //                return _enumStringValueMap[value];
    //            }
    //        }

    //        public EnumInfo this[int value]
    //        {
    //            get
    //            {
    //                return _enumIntValueMap[value];
    //            }
    //        }

    //        public static IEnumerable<EnumInfo> Values
    //        {
    //            get
    //            {
    //                return _enumStringValueMap.Values;
    //            }
    //        }

    //        static EnumInfoCollection()
    //        {
    //            var values = Enum.GetValues(_enumType);

    //            _enumStringValueMap = new Dictionary<string, EnumInfo>(values.Length, StringComparer.InvariantCultureIgnoreCase);
    //            _enumIntValueMap = new Dictionary<int, EnumInfo>(values.Length);

    //            foreach (TEnum v in values)
    //            {
    //                var info = GetEnumInfo(v);
    //                _enumStringValueMap.Add(v.ToString(), info);
    //                _enumIntValueMap.Add(info.NumericValue, info);
    //            }
    //        }

    //        private static EnumInfo GetEnumInfo(TEnum value)
    //        {
    //            var fieldInfo = _enumType.GetField(value.ToString());

    //            var info = new EnumInfo()
    //            {
    //                Value = value,
    //                NumericValue = (int)fieldInfo.GetValue(value),
    //                Name = value.ToString(),
    //                DisplayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>(false)
    //            };
    //            return info;
    //        }

    //        public class EnumInfo
    //        {
    //            public Enum Value { get; internal set; }
    //            public string Name { get; internal set; }
    //            public int NumericValue { get; internal set; }
    //            public DisplayAttribute DisplayAttribute { get; internal set; }

    //            public string Display
    //            {
    //                get
    //                {
    //                    if (DisplayAttribute == null || DisplayAttribute.Name == null) return Name;
    //                    return DisplayAttribute.Name;
    //                }
    //            }
    //        }
    //    }
    //}

    //public class EnumHelper
    //{
    //    public static IEnumerable<Enum> GetFlags(Enum input)
    //    {
    //        foreach (Enum value in Enum.GetValues(input.GetType()))
    //            if (input.HasFlag(value))
    //                yield return value;
    //    }

    //    public static Enum Parse(string value)
    //    {
    //        if (string.IsNullOrEmpty(value) || !_enumStringValueMap.ContainsKey(value)) return _enumStringValueMap.Values.First().Value;
    //        return _enumStringValueMap[value].Value;
    //    }

    //    public static string GetName(int value)
    //    {
    //        return _enumIntValueMap[value].Name;
    //    }

    //    public static IEnumerable<string> GetNames()
    //    {
    //        return _enumStringValueMap.Values.Select(v => v.Name);
    //    }

    //    public static string GetDisplayValue(T value)
    //    {
    //        return _enumStringValueMap[value.ToString()].Display;
    //    }

    //    public static string GetDisplayValue(string value)
    //    {
    //        return _enumStringValueMap[value].Display;
    //    }

    //    public static string GetDisplayValue(int value)
    //    {
    //        return _enumIntValueMap[value].Display;
    //    }

    //    public static IEnumerable<string> GetDisplayValues()
    //    {
    //        return _enumStringValueMap.Values.Select(v => v.Display);
    //    }

    //    public static EnumInfo GetInfo(T value)
    //    {
    //        return _enumStringValueMap[value.ToString()];
    //    }

    //    public static EnumInfo GetInfo(string value)
    //    {
    //        return _enumStringValueMap[value];
    //    }

    //    public static EnumInfo GetInfo(int value)
    //    {
    //        return _enumIntValueMap[value];
    //    }

    //    private static EnumInfo GetEnumInfo(T value)
    //    {
    //        var fieldInfo = _enumType.GetField(value.ToString());

    //        var info = new EnumInfo()
    //        {
    //            Value = value,
    //            NumericValue = (int)fieldInfo.GetValue(value),
    //            Name = value.ToString(),
    //            DisplayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>(false)
    //        };
    //        return info;
    //    }

    //    public class EnumInfo
    //    {
    //        public Enum Value { get; internal set; }
    //        public string Name { get; internal set; }
    //        public int NumericValue { get; internal set; }
    //        public DisplayAttribute DisplayAttribute { get; internal set; }

    //        public string Display
    //        {
    //            get
    //            {
    //                if (DisplayAttribute == null || DisplayAttribute.Name == null) return Name;
    //                return DisplayAttribute.Name;
    //            }
    //        }

    //        internal EnumInfo(Enum value)
    //        {
    //            Value = value;
    //            Name = value.ToString();
                
    //            var fieldInfo = value.GetType().GetField(value.ToString());
    //            NumericValue = (int)fieldInfo.GetValue(value);
    //            DisplayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>(false);
    //        }
    //    }

    //    public class EnumInfoCollection
    //    {
    //        internal static Dictionary<string, EnumInfo> _enumStringValueMap;
    //        internal static Dictionary<int, EnumInfo> _enumIntValueMap;

    //        public EnumInfo this[string value]
    //        {
    //            get
    //            {
    //                return _enumStringValueMap[value];
    //            }
    //        }

    //        public EnumInfo this[int value]
    //        {
    //            get
    //            {
    //                return _enumIntValueMap[value];
    //            }
    //        }

    //        public static IEnumerable<EnumInfo> Values
    //        {
    //            get
    //            {
    //                return _enumStringValueMap.Values;
    //            }
    //        }

    //        static EnumInfoCollection()
    //        {
    //            var values = Enum.GetValues(_enumType);

    //            _enumStringValueMap = new Dictionary<string, EnumInfo>(values.Length, StringComparer.InvariantCultureIgnoreCase);
    //            _enumIntValueMap = new Dictionary<int, EnumInfo>(values.Length);

    //            foreach (TEnum v in values)
    //            {
    //                var info = GetEnumInfo(v);
    //                _enumStringValueMap.Add(v.ToString(), info);
    //                _enumIntValueMap.Add(info.NumericValue, info);
    //            }
    //        }

    //        private static EnumInfo GetEnumInfo(TEnum value)
    //        {
    //            var fieldInfo = _enumType.GetField(value.ToString());

    //            var info = new EnumInfo()
    //            {
    //                Value = value,
    //                NumericValue = (int)fieldInfo.GetValue(value),
    //                Name = value.ToString(),
    //                DisplayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>(false)
    //            };
    //            return info;
    //        }
    //    }

    //}
}
