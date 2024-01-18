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
    // ExpenceRespondRequest sınıfının validasyonunun yapıldığı Validator
    public class ExpenceRespondRequestValidator : AbstractValidator<ExpenceRespondRequest>
    {
        public ExpenceRespondRequestValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.ExpenceNotifyId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.isApproved).NotNull();
            RuleFor(x => x.Explanation).NotNull().NotEmpty().MaximumLength(100);
        }
    }

}
