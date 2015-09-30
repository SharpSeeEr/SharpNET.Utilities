using HtmlTags;
using SharpNET.Utilities;
using SharpNET.Utilities.Web.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SharpNET.Utilities.Web.Helpers.Bootstrap
{
    public static class FormHelpers
    {
        public static HtmlTag BsCheckbox(this HtmlHelper htmlHelper, string name, object value, string label, bool isChecked = false)
        {
            var checkboxTag = new BsCheckboxTag(label, isChecked)
                .Name(name)
                .Value(value.ToString());
            return checkboxTag;
        }

        public static HtmlTag BsRadio(this HtmlHelper htmlHelper, string name, object value, string label, bool isChecked = false)
        {
            var radioTag = new BsRadioTag(label, isChecked)
                .Name(name)
                .Value(value.ToString());
            return radioTag;
        }

        public static HtmlTag BsCheckboxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression)
        {
            var helperInfo = new HtmlHelperInfo<TModel, bool>(htmlHelper, expression);

            var isChecked = (bool)helperInfo.Model;

            var checkboxTag = new BsCheckboxTag(helperInfo.Label, helperInfo.Model)
                .Name(helperInfo.HtmlFullName)
                .Value("true")
                .AddValidationAttributes(htmlHelper, helperInfo);

            checkboxTag.Append(new HiddenTag().Name(helperInfo.HtmlName).Value("false"));
            
            return checkboxTag;
        }

        
        public static HtmlTag BsRadioFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, TProperty value, string label)
        {
            var helperInfo = new HtmlHelperInfo<TModel, TProperty>(htmlHelper, expression);

            var tag = new BsRadioTag(label, helperInfo.Model.ToString() == value.ToString())
                .Name(helperInfo.HtmlFullName)
                .Value(value.ToString())
                .AddValidationAttributes(htmlHelper, helperInfo);

            return tag;
        }

        public static HtmlTag BsCheckboxListFromEnum<T>(this HtmlHelper htmlHelper, string name, IEnumerable<T> values)
        {
            var divTag = new DivTag().AddClass("list");
            int index = 0;

            foreach (T item in Enum.GetValues(typeof(T)))
            {
                divTag.Append(BsCheckbox(htmlHelper, name + "[" + index + "]", item, EnumHelper<T>.GetDisplayValue(item), values.Contains(item)));
            }

            return divTag;
        }

        public static HtmlTag BsCheckboxListFromEnum(this HtmlHelper htmlHelper, string name, Enum value)
        {
            var divTag = new DivTag().AddClass("list");

            foreach (Enum item in Enum.GetValues(value.GetType()))
            {
                divTag.AppendHtml(BsCheckbox(htmlHelper, name, item, item.Display(), value.HasFlag(item)).ToString());
            }

            return divTag;
        }

        public static HtmlTag BsCheckboxListFromEnumFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            var helperInfo = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            
            var divTag = new DivTag();

            var propValue = (Enum)helperInfo.Model;
            foreach (Enum item in Enum.GetValues(helperInfo.ModelType))
            {
                divTag.Append(BsCheckbox(htmlHelper, fullName, item, item.Display(), propValue.HasFlag(item)));
            }

            return divTag;
        }

        public static HtmlTag BsCheckboxListFromEnumListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            var helperInfo = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            var divTag = new DivTag();

            var propValue = (IEnumerable<TProperty>)helperInfo.Model;
            return BsCheckboxListFromEnum(htmlHelper, fullName, propValue);
            //int index = 0;
            //foreach (Enum item in Enum.GetValues(helperInfo.ModelType))
            //{
            //    divTag.Append(BsCheckbox(htmlHelper, fullName + "[" + index + "]", item, item.Display(), propValue.Contains(item)));
            //    index += 1;
            //}

            //return divTag;
        }

        public static HtmlTag BsRadioListFromEnum(this HtmlHelper htmlHelper, string name, Enum value)
        {
            var divTag = new DivTag().AddClass("list");

            foreach (Enum item in Enum.GetValues(value.GetType()))
            {
                divTag.Append(BsRadio(htmlHelper, name, item, item.Display(), value == item));
            }

            return divTag;
        }

        public static HtmlTag BsRadioListFromEnumFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            var divTag = new DivTag().AddClass("list");

            var propValue = (Enum)metadata.Model;
            foreach (Enum item in Enum.GetValues(metadata.ModelType))
            {
                divTag.Append(BsRadio(htmlHelper, fullName, item, item.Display(), propValue == item));
            }

            return divTag;
        }

        public static HtmlTag BsTextBox(this HtmlHelper htmlHelper, string name, object value)
        {
            return new TextboxTag().Name(name).Value(value as string).AddClass("form-control");
        }

        public static HtmlTag BsTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var helperInfo = new HtmlHelperInfo<TModel, TProperty>(htmlHelper, expression);

            return BsTextBox(htmlHelper, helperInfo.HtmlFullName, helperInfo.Model);
        }

        public static HtmlTag BsTextArea(this HtmlHelper htmlHelper, string name, object value)
        {
            return new HtmlTag("textarea")
                .Name(name)
                .AddClass("form-control")
                .AppendHtml(value as string);
        }

        public static HtmlTag BsTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var helperInfo = new HtmlHelperInfo<TModel, TProperty>(htmlHelper, expression);

            return BsTextArea(htmlHelper, helperInfo.HtmlFullName, helperInfo.Model);
        }

        public static HtmlTag BsPassword(this HtmlHelper htmlHelper, string name, object value)
        {
            return BsTextBox(htmlHelper, name, value).Attr("type", "password");
        }

        public static HtmlTag BsPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var helperInfo = new HtmlHelperInfo<TModel, TProperty>(htmlHelper, expression);
            return BsPassword(htmlHelper, helperInfo.HtmlFullName, helperInfo.Model)
                .AddValidationAttributes(htmlHelper, helperInfo);
        }

        public static HtmlTag BsLoginPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string validationMessage = "")
        {
            if (string.IsNullOrEmpty(validationMessage)) validationMessage = "The Password Field Is Required";

            return BsPasswordFor(htmlHelper, expression)
                //.Data("val", true)
                //.Data("val-required", validationMessage)
                //.Attr("aria-required", true)
                //.Attr("aria-invalid", false)
                //.Attr("aria-describedby", "Password-error")
                .Attr("placeholder", "Password");
        }

        public static HtmlTag BsControlLabel(this HtmlHelper htmlHelper, string label)
        {
            return new HtmlTag("label")
                .AddClass("control-label")
                .AppendHtml(label);
        }

        public static HtmlTag BsControlLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var helperInfo = new HtmlHelperInfo<TModel, TProperty>(htmlHelper, expression);

            return BsControlLabel(htmlHelper, helperInfo.Label);
        }

        public static HtmlTag BsDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel = null)
        {
            var tag = new SelectTag();
            tag.Name(name).AddClass("form-control");
            if (optionLabel != null) tag.DefaultOption(optionLabel);

            foreach (var item in selectList)
            {
                tag.Option(item.Text, item.Value);
                if (item.Selected) tag.SelectByValue(item.Value);
            }

            return tag;
        }

        public static HtmlTag BsDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel = null)
        {
            var helperInfo = new HtmlHelperInfo<TModel, TProperty>(htmlHelper, expression);
            if (helperInfo.Model != null)
            {
                var selected = selectList.FirstOrDefault(e => e.Value == helperInfo.Model.ToString());
                if (selected != null) selected.Selected = true;
            }

            return BsDropDownList(htmlHelper, helperInfo.HtmlFullName, selectList, optionLabel)
                .AddValidationAttributes(htmlHelper, helperInfo);
        }

        public static HtmlTag AddValidationAttributes<TModel, TProperty>(this HtmlTag htmlTag, HtmlHelper<TModel> htmlHelper, HtmlHelperInfo<TModel, TProperty> helperInfo)
        {
            return htmlTag.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(helperInfo.HtmlFullName, helperInfo.Metadata));
        }

        public static HtmlTag MergeAttributes(this HtmlTag htmlTag, IDictionary<string, object> attributes)
        {
            foreach (var kvp in attributes)
            {
                htmlTag.Attr(kvp.Key, kvp.Value);
            }
            return htmlTag;
        }
    }
}
