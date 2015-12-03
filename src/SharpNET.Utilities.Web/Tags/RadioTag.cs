using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharpNET.Utilities.Web.Tags
{
    public class RadioTag : HtmlTags.HtmlTag
    {
        public RadioTag(bool isChecked)
            : base("input")
        {
            Attr("type", "radio");
            if (isChecked)
            {
                Attr("checked", "true");
            }
        }

        //public bool IsChecked
        //{
        //    get
        //    {
        //        return HasAttr("checked") && Attr("checked") == "true";
        //    }
        //    set
        //    {
        //        if (value)
        //        {
        //            Attr("checked", "true");
        //        }
        //        else
        //        {
        //            RemoveAttr("checked");
        //        }
        //    }
        //}
    }
}