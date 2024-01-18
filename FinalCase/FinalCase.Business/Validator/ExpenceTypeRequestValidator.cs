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
    // ExpenceTypeRequest sınıfının validasyonunun yapıldığı Validator
    public class ExpenceTypeRequestValidator : AbstractValidator<ExpenceTypeRequest>
    {
        public ExpenceTypeRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(250);
        }
    }

}
