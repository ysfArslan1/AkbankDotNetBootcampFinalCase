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
    // ExpenceNotifyRequest sınıfının validasyonunun yapıldığı Validator
    public class ExpenceNotifyRequestValidator : AbstractValidator<ExpenceNotifyRequest>
    {
        public ExpenceNotifyRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.ExpenceTypeId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Amount).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Explanation).NotNull().NotEmpty().MaximumLength(250);
            RuleFor(x => x.TransferType).NotNull().NotEmpty().MaximumLength(100);
        }
    }

}
