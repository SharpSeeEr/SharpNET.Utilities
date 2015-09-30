using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SharpNET.Utilities.Web.Helpers
{
    public class HtmlHelperInfo<TModel, TProperty>
    {
        public string HtmlId { get; private set; }
        public string HtmlName { get; private set; }
        public string HtmlFullName { get; private set; }
        public TProperty Model { get; private set; }
        public string Label { get; private set; }

        public ModelMetadata Metadata { get; private set; }

        public HtmlHelperInfo(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            HtmlName = ExpressionHelper.GetExpressionText(expression);
            HtmlFullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(HtmlName);

            Metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            //HtmlId = Metadata.
            if (Metadata.Model != null) Model = (TProperty)Metadata.Model;
            Label = Metadata.DisplayName ?? Metadata.PropertyName;
        }
    }
}
