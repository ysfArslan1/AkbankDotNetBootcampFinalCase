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
    // ExpencePaymentRequest sınıfının validasyonunun yapıldığı Validator
    public class ExpencePaymentRequestValidator : AbstractValidator<ExpencePaymentRequest>
    {
        public ExpencePaymentRequestValidator()
        {
            RuleFor(x => x.AccountId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.ExpenceRespondId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.TransactionDate).NotNull().NotEmpty().LessThan(DateTime.Now.AddDays(-10);
            RuleFor(x => x.IsDeposited).NotNull();
            RuleFor(x => x.TransferType).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(250);
        }
    }

}
