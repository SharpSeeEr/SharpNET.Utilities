using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.Utilities
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the Description attribute of the DisplayAttribute on the enum value.
        /// If there is no DisplayAttribute or no Description provided the enum's string value is returned
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Description(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        /// <summary>
        /// Returns the Name attribute of the DisplayAttribute on the enum value.
        /// If there is no DisplayAttribute or no Name provided the enum's string value is returned
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Display(this Enum value)
        {
            string stringValue = value.ToString();
            FieldInfo fi = value.GetType().GetField(stringValue);
            if (fi == null) return stringValue;
            var attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);
            return attributes.Length > 0 ? attributes[0].Name : stringValue;
        }

        /// <summary>
        /// Returns the GroupName attribute of the DisplayAttribute on the enum value.
        /// If there is no DisplayAttribute or no Description provided the enum's string value is returned
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GroupName(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            if (fi == null) return value.ToString();
            var attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);
            return attributes.Length > 0 ? attributes[0].GroupName : value.ToString();
        }
    }
}
