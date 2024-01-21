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
    // CreateExpenceTypeRequest sınıfının validasyonunun yapıldığı Validator
    public class CreateExpenceTypeRequestValidator : AbstractValidator<CreateExpenceTypeRequest>
    {
        public CreateExpenceTypeRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(250);
        }
    }

    // UpdateExpenceTypeRequest sınıfının validasyonunun yapıldığı Validator
    public class UpdateExpenceTypeRequestValidator : AbstractValidator<UpdateExpenceTypeRequest>
    {
        public UpdateExpenceTypeRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(200);
        }
    }

}
