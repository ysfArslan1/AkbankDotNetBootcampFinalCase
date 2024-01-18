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
    // RoleRequest sınıfının validasyonunun yapıldığı Validator
    public class RoleRequestValidator : AbstractValidator<RoleRequest>
    {
        public RoleRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(100);
        }
    }

}
