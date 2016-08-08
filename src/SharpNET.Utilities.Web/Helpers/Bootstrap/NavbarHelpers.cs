using HtmlTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SharpNET.Utilities.Web.Helpers.Bootstrap
{
    public static class NavbarHelpers
    {
        public static HtmlTag NavItem(this HtmlHelper htmlHelper, string label, string actionName, string controllerName)
        {
            string cssClass = htmlHelper.ViewContext.RouteData.Values["controller"].ToString() == controllerName ? "active" : "";

            var li = new HtmlTag("li")
                .AddClass(cssClass);
            li.AppendHtml(htmlHelper.ActionLink(label, actionName, controllerName).ToString());
            return li;
        }

        public static HtmlTag NavItem(this HtmlHelper htmlHelper, string label, string actionName)
        {
            string cssClass = actionName.StartsWith(htmlHelper.ViewContext.RouteData.Values["action"].ToString()) ? "active" : "";

            var li = new HtmlTag("li")
                .AddClass(cssClass);
            li.AppendHtml(htmlHelper.ActionLink(label, actionName).ToString());
            return li;
        }
        public static HtmlTag NavItem(this HtmlHelper htmlHelper, string label, string actionName, string controllerName, string areaName)
        {
            string cssClass = htmlHelper.ViewContext.RouteData.Values["controller"].ToString() == controllerName ? "active" : "";

            var li = new HtmlTag("li")
                .AddClass(cssClass);
            li.AppendHtml(htmlHelper.ActionLink(label, actionName, controllerName, new { area = areaName }, new { }).ToString());
            return li;
        }
    }


}
