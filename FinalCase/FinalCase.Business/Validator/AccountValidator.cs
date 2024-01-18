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
    // CreateAccountRequest sınıfının validasyonunun yapıldığı Validator
    public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
    {
        public CreateAccountRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Balance).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.CurrencyType).NotNull().NotEmpty().MaximumLength(3);
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
        }
    }

    // UpdateAccountRequest sınıfının validasyonunun yapıldığı Validator
    public class UpdateAccountRequestValidator : AbstractValidator<UpdateAccountRequest>
    {
        public UpdateAccountRequestValidator()
        {
            RuleFor(x => x.Balance).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
        }
    }

}
