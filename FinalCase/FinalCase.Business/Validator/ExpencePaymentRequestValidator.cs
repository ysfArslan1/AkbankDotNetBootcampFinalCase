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
    // CreateExpencePaymentRequest sınıfının validasyonunun yapıldığı Validator
    public class CreateExpencePaymentRequestValidator : AbstractValidator<CreateExpencePaymentRequest>
    {
        public CreateExpencePaymentRequestValidator()
        {
            RuleFor(x => x.AccountId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.ExpenceRespondId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.TransactionDate).NotNull().NotEmpty().LessThan(DateTime.Now);
            RuleFor(x => x.ReceiverId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.ReceiverName).NotNull().NotEmpty().MaximumLength(200);
        }
    }
    // UpdateExpencePaymentRequest sınıfının validasyonunun yapıldığı Validator
    public class UpdateExpencePaymentRequestValidator : AbstractValidator<UpdateExpencePaymentRequest>
    {
        public UpdateExpencePaymentRequestValidator()
        {
            RuleFor(x => x.TransactionDate).NotNull().NotEmpty().LessThan(DateTime.Now);
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(250);
        }
    }

}
