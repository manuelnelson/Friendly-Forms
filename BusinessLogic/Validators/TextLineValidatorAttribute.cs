using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessLogic.Properties;
using System.Web.Mvc;
namespace BusinessLogic.Validators
{
    public class TextLineInputValidatorAttribute : RegularExpressionAttribute, IClientValidatable
    {
        public TextLineInputValidatorAttribute()
            : base(Resources.TextLineInputValidatorRegEx)
        {
            this.ErrorMessage = Resources.InvalidInputCharacter;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = Resources.InvalidInputCharacter,
                ValidationType = "textlineinput"
            };

            rule.ValidationParameters.Add("pattern", Resources.TextLineInputValidatorRegEx);
            return new List<ModelClientValidationRule>() { rule };
        }
    }
}
