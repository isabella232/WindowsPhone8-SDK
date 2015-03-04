using System;
using Telerik.Windows.Controls;

namespace Telerik.DesignTemplates.WP.Helpers
{
    public class MinLengthValidator : DataFormValidator
    {
        private const string PasswordValidationMessage = "Password must be at least 6 characters.";
        private const int MinLength = 6;

        public override void Validate(ValidatingDataFieldEventArgs args)
        {
            var passwordValue = (string)args.AssociatedDataField.Value;
            if (passwordValue.Length < MinLength)
            {
                args.IsInputValid = false;
                args.ValidationMessage = PasswordValidationMessage;
            }
        }
    }
}