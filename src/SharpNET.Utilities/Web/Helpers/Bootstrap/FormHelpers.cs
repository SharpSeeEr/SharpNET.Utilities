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
            var metadata = new HtmlHelperInfo<TModel, bool>(htmlHelper, expression);

            var isChecked = (bool)metadata.Model;

            var checkboxTag = new BsCheckboxTag(metadata.Label, metadata.Model)
                .Name(metadata.HtmlFullName)
                .Value("true");

            checkboxTag.Append(new HiddenTag().Name(metadata.HtmlName).Value("false"));
            
            return checkboxTag;
        }

        
        public static HtmlTag BsRadioFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, TProperty value)
        {
            var metadata = new HtmlHelperInfo<TModel, TProperty>(htmlHelper, expression);

            var tag = new BsRadioTag(metadata.Label, metadata.Model.ToString() == value.ToString())
                .Name(metadata.HtmlFullName)
                .Value(value.ToString());

            return tag;
        }

        public static HtmlTag CheckboxListFromEnum(this HtmlHelper htmlHelper, string name, Enum value)
        {
            var divTag = new DivTag().AddClass("list");

            foreach (Enum item in Enum.GetValues(value.GetType()))
            {
                divTag.AppendHtml(BsCheckbox(htmlHelper, name, item, item.Display(), value.HasFlag(item)).ToString());
            }

            return divTag;
        }

        public static HtmlTag CheckboxListFromEnumFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            
            var divTag = new DivTag();

            var propValue = (Enum)metadata.Model;
            foreach (Enum item in Enum.GetValues(metadata.ModelType))
            {
                divTag.Append(BsCheckbox(htmlHelper, fullName, item, item.Display(), propValue.HasFlag(item)));
            }

            return divTag;
        }

        public static HtmlTag RadioListFromEnum(this HtmlHelper htmlHelper, string name, Enum value)
        {
            var divTag = new DivTag().AddClass("list");

            foreach (Enum item in Enum.GetValues(value.GetType()))
            {
                divTag.Append(BsRadio(htmlHelper, name, item, item.Display(), value == item));
            }

            return divTag;
        }

        public static HtmlTag RadioListFromEnumFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
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
            return new TextboxTag().Name(name).Value(value.ToString()).AddClass("form-control");
        }

        public static HtmlTag BsTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var metadata = new HtmlHelperInfo<TModel, TProperty>(htmlHelper, expression);

            return BsTextBox(htmlHelper, metadata.HtmlFullName, metadata.Model);
        }

        public static HtmlTag BsTextArea(this HtmlHelper htmlHelper, string name, object value)
        {
            return new HtmlTag("textarea")
                .Name(name)
                .AddClass("form-control")
                .AppendHtml(value.ToString());
        }

        public static HtmlTag BsTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var metadata = new HtmlHelperInfo<TModel, TProperty>(htmlHelper, expression);

            return BsTextArea(htmlHelper, metadata.HtmlFullName, metadata.Model);
        }

        public static HtmlTag BsControlLabel(this HtmlHelper htmlHelper, string label)
        {
            return new HtmlTag("label")
                .AddClass("control-label")
                .AppendHtml(label);
        }

        public static HtmlTag BsControlLabelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var metadata = new HtmlHelperInfo<TModel, TProperty>(htmlHelper, expression);

            return BsControlLabel(htmlHelper, metadata.Label);
        }

        public static HtmlTag BsDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel)
        {
            var tag = new SelectTag();
            tag.Name(name);
            tag.DefaultOption(optionLabel);

            foreach (var item in selectList)
            {
                tag.Option(item.Text, item.Value);
                if (item.Selected) tag.SelectByValue(item.Value);
            }

            return tag;
        }

        public static HtmlTag BsDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel)
        {
            var metadata = new HtmlHelperInfo<TModel, TProperty>(htmlHelper, expression);
            var selected = selectList.FirstOrDefault(e => e.Value == metadata.Model.ToString());
            if (selected != null) selected.Selected = true;
            return BsDropDownList(htmlHelper, metadata.HtmlFullName, selectList, optionLabel);
        }
    }
}
