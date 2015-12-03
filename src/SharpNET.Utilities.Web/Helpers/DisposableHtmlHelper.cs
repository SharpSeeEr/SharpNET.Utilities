using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SharpNET.Utilities.Web.Helpers
{
    internal abstract class DisposableHtmlHelper : IDisposable
    {
        protected readonly HtmlHelper _htmlHelper;

        public DisposableHtmlHelper(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public abstract void Dispose();
    }
}
