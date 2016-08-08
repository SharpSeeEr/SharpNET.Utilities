using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SharpNET.Utilities.Web.Validation
{
    public class RequiredCheckedAttribute : ValidationAttribute, IClientValidatable
    {
        public RequiredCheckedAttribute(string ErrorMessage = "Must be checked")
            : base(ErrorMessage)
        {
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = ErrorMessage;
            rule.ValidationType = "requiredchecked";

            yield return rule;
        }

        public override bool IsValid(object value)
        {
            return value is bool && (bool)value;
        }
    }
}
