using FinalCase.Schema;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalCase.Business.Validator
{
    public class ContactRequestValidator : AbstractValidator<ContactRequest>
    {
        public ContactRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(100).Must(ValidateEmail);
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().MaximumLength(11).Must(ValidatePhoneNumber);
        }
        // Telefon numarası doğrulaması için kullanılan metot
        private bool ValidatePhoneNumber(string text)
        {
            var regex = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            return regex.IsMatch(text);
        }

        // Email doğrulaması için kullanılan metot
        private bool ValidateEmail(string text)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(text);
        }
    }

}
