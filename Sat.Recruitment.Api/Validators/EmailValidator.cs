using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace Sat.Recruitment.Api.Validators
{
    public class EmailValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        private const string emailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        public override string Name => "EmailValidator";

        bool IsValidMailAddress(string email)
            => string.IsNullOrEmpty(email)
            && Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase);

        public override bool IsValid(ValidationContext<T> context, TProperty value)
        => this.IsValidMailAddress(value as string);

        protected override string GetDefaultMessageTemplate(string errorCode)
            => ErrorMessages.EmailIsNotValid;
    }

    public static class ValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> ValidEmail<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new EmailValidator<T, TProperty>());
        }
    }
}