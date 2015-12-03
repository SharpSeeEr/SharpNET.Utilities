using HtmlTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SharpNET.Utilities.Web.Helpers.Bootstrap
{
    public static class BootstrapHelpers
    {
        public static HtmlTag DebugGrid(this HtmlHelper htmlHelper)
        {
            var tag = new DivTag();
            tag.Append(new DivTag().AddClass("alert alert-info text-center visible-xs").Text("Extra Small"))
                .Append(new DivTag().AddClass("alert alert-info text-center visible-sm").Text("Small"))
                .Append(new DivTag().AddClass("alert alert-info text-center visible-md").Text("Medium"))
                .Append(new DivTag().AddClass("alert alert-info text-center visible-lg").Text("Large"));
            return tag;

        }
    }
}
