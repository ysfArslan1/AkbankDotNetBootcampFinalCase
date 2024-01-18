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
    // DocumentRequest sınıfının validasyonunun yapıldığı Validator
    public class DocumentRequestValidator : AbstractValidator<DocumentRequest>
    {
        public DocumentRequestValidator()
        {
            RuleFor(x => x.ExpenceNotifyId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(250);
            RuleFor(x => x.Content).NotNull();
        }
    }

}
