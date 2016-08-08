using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SharpNET.Utilities.Web
{
    public class JsonNetController : Controller
    {
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        protected JsonNetResult JsonNet(object data, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return new JsonNetResult
            {
                Data = data,
                JsonRequestBehavior = behavior
            };
        }
    }
}
