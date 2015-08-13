using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace SharpNET.Utilities.Web.Helpers.Bootstrap
{
    public static class PanelHelpers
    {
        public static IDisposable BeginPanel(this HtmlHelper htmlHelper,
            PanelType panelType = PanelType.basic, string heading = "", string title = "",
            string footer = "", dynamic attributes = null)
        {
            return new Panel(htmlHelper, panelType, heading, title, "h3", footer, attributes);
        }
    }

    public enum PanelType
    {
        basic,
        primary,
        success,
        info,
        warning,
        danger
    }

    

    internal class Panel : DisposableHtmlHelper
    {
        public string Heading { get; set; }
        public string Title { get; set; }
        public string Footer { get; set; }

        public PanelType Type { get; set; }

        public Panel(HtmlHelper htmlHelper, PanelType panelType = PanelType.basic, string heading = "", string title = "", string titleTag = "h3", string footer = "", dynamic attributes = null)
            : base(htmlHelper)
        {
            var writer = htmlHelper.ViewContext.Writer;

            var panelContainer = new TagBuilder("div");
            panelContainer.MergeAttributes<string, object>(attributes);
            
            if (panelType == PanelType.basic) panelContainer.AddCssClass("panel-default");
            else panelContainer.AddCssClass("panel-" + panelType);
            
            panelContainer.AddCssClass("panel");

            writer.WriteLine(panelContainer.ToString(TagRenderMode.StartTag));

            if (!string.IsNullOrEmpty(heading) || !string.IsNullOrEmpty(title))
            {
                var headingDiv = new TagBuilder("div");
                headingDiv.AddCssClass("panel-heading");
                writer.Write(headingDiv.ToString(TagRenderMode.StartTag));
                if (!string.IsNullOrEmpty(title))
                {
                    writer.WriteLine();
                    var titleTagBuilder = new TagBuilder(titleTag);
                    titleTagBuilder.AddCssClass("panel-title");
                    titleTagBuilder.SetInnerText(title);
                    writer.WriteLine(titleTagBuilder.ToString());
                }
                else if (!string.IsNullOrEmpty(heading)) 
                {
                    writer.Write(heading);
                }
                writer.WriteLine(headingDiv.ToString(TagRenderMode.EndTag));
            }
            writer.WriteLine("<div class=\"panel-body\">");
        }

        public override void Dispose()
        {
            _htmlHelper.ViewContext.Writer.WriteLine("</div></div>");
        }
    }
}