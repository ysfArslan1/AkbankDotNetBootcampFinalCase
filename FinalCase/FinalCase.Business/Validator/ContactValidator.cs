using FinalCase.Schema;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalCase.Business.Validator
{
    public class CreateContactValidator : AbstractValidator<ContactRequest>
    {
        public CreateContactValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Phone).NotNull().NotEmpty().MaximumLength(100);
        }
    }

    public class UpdateContactValidator : AbstractValidator<ContactRequest>
    {
        public UpdateContactValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Email).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Phone).NotNull().NotEmpty().MaximumLength(100);
        }
    }
}
