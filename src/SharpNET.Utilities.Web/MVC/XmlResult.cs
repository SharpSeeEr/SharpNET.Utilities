using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace SharpNET.Utilities.Web
{
    public sealed class XmlResult : ActionResult
    {
        private object _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlResult"/> class.
        /// </summary>
        /// <param name="model">The object to serialize to XML.</param>
        public XmlResult(object model)
        {
            _model = model;
        }

        /// <summary>
        /// Gets the object to be serialized to XML.
        /// </summary>
        public object Model
        {
            get { return _model; }
        }

        /// <summary>
        /// Serialises the object that was passed into the constructor to XML and writes the corresponding XML to the result stream.
        /// </summary>
        /// <param name="context">The controller context for the current request.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (_model != null)
            {
                context.HttpContext.Response.Clear();
                var xs = new System.Xml.Serialization.XmlSerializer(_model.GetType());
                context.HttpContext.Response.ContentType = "text/xml";
                xs.Serialize(context.HttpContext.Response.Output, _model);
            }
        }
    }
}
