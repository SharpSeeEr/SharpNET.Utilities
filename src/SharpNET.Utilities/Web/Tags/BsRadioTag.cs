using HtmlTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.Utilities.Web.Tags
{
    public class BsRadioTag : RadioTag
    {
        private string _label;
        public string Label() { return _label; }
        public void Label(string label) { _label = label; }

        public BsRadioTag(string label, bool isChecked)
            : base(isChecked)
        {
            _label = label;
        }

        public override string ToString()
        {

            var labelTag = WrapWith(new HtmlTag("label"));
            labelTag.AppendHtml(_label);

            var outerDiv = labelTag.WrapWith(new DivTag()).AddClass("radio").ToString();

            return outerDiv.ToString();
            //<div class="radio">
            //  <label>
            //    <input type="radio" name="Radio1" id="Radio1" value="value">
            //    Radio Label
            //  </label>
            //</div>
        }
    }
}
