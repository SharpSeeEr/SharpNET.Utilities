using HtmlTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SharpNET.Utilities.Web.Helpers
{
    public static class GeneralHtmlHelpers
    {
        public static string YesNo(this HtmlHelper htmlHelper, bool value)
        {
            return value ? "Yes" : "No";
        }

        public static string YesNo(this HtmlHelper htmlHelper, bool? value)
        {
            return value.HasValue && value.Value ? "Yes" : "No";
        }

        public static HtmlTag Glyphicon(this HtmlHelper htmlHelper, string icon)
        {
            return new HtmlTag("span").AddClass("glyphicon").AddClass("glyphicon-" + icon);
        }

        public static HtmlTag FontAwesome(this HtmlHelper htmlHelper, string icon, params string[] options)
        {
            var tag = new HtmlTag("span").AddClass("fa").AddClass(FAPrefix(icon));
            foreach (var item in options)
            {
                tag.AddClass(FAPrefix(item));
            }
            return tag;
        }

        private static string FAPrefix(string text)
        {
            if (text.StartsWith("fa-")) return text;
            return "fa-" + text;
        }
    }
}