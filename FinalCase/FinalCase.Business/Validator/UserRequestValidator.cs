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
    // CreateUserRequest sınıfının validasyonunun yapıldığı Validator
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.IdentityNumber).NotNull().NotEmpty().Length(11);
            RuleFor(x => x.FirstName).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.daDateOfBirtht).NotNull().NotEmpty().LessThan(DateTime.Now);
            RuleFor(x => x.LastActivityDate).NotNull().NotEmpty().LessThan(DateTime.Now);
            RuleFor(x => x.RoleId).NotNull().NotEmpty().GreaterThan(0);
        }
    }
    // UpdateUserRequest sınıfının validasyonunun yapıldığı Validator
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.daDateOfBirtht).NotNull().NotEmpty().LessThan(DateTime.Now);
            RuleFor(x => x.RoleId).NotNull().NotEmpty().GreaterThan(0);
        }
    }

}
