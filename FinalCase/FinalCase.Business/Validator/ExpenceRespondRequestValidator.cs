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
    // CreateExpenceRespondRequest sınıfının validasyonunun yapıldığı Validator
    public class CreateExpenceRespondRequestValidator : AbstractValidator<CreateExpenceRespondRequest>
    {
        public CreateExpenceRespondRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.ExpenceNotifyId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.isApproved).NotNull();
            RuleFor(x => x.IsDeposited).NotNull();
            RuleFor(x => x.Explanation).NotNull().NotEmpty().MaximumLength(100);
        }
    }
    // UpdateExpenceRespondRequest sınıfının validasyonunun yapıldığı Validator
    public class UpdateExpenceRespondRequestValidator : AbstractValidator<UpdateExpenceRespondRequest>
    {
        public UpdateExpenceRespondRequestValidator()
        {
            RuleFor(x => x.isApproved).NotNull();
            RuleFor(x => x.IsDeposited).NotNull();
            RuleFor(x => x.Explanation).NotNull().NotEmpty().MaximumLength(100);
        }
    }

}
