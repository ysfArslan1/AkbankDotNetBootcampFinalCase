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
    // AccountRequest sınıfının validasyonunun yapıldığı Validator
    public class AccountRequestValidator : AbstractValidator<AccountRequest>
    {
        public AccountRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Balance).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.CurrencyType).NotNull().NotEmpty().MaximumLength(3);
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
        }
    }

}
