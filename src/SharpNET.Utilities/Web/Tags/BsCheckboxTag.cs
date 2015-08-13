using HtmlTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace SharpNET.Utilities.Web.Tags
{
    public class BsCheckboxTag : CheckboxTag
    {
        private string _label;
        public string Label() { return _label; }
        public void Label(string label) { _label = label; }

        public BsCheckboxTag(string label, bool isChecked)
            : base(isChecked)
        {
            _label = label;
            RenderFromTop();
        }

        private bool _writeWrapper = true;
        protected override void writeHtml(HtmlTextWriter html)
        {
            if (!WillBeRendered()) return;

            if (_writeWrapper)
            {
                var labelTag = WrapWith(new HtmlTag("label"));
                labelTag.AppendHtml(_label);

                var outerDiv = labelTag.WrapWith(new DivTag()).AddClass("checkbox");

                _writeWrapper = false;
                outerDiv.ToString(html);
                _writeWrapper = true;
            }
            else
            {
                base.writeHtml(html);
            }
           
        }

    }
}
