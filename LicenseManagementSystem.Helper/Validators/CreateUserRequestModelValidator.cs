using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Helper.Validators
{
    public class CreateUserRequestModelValidator : AbstractValidator<CreateUserRequestModel>
    {
        public CreateUserRequestModelValidator()
        {
            RuleFor(p => p.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Matches(Constants.EmailRegex).WithMessage("Invalid email format.");
            RuleFor(p => p.UserName).NotEmpty().WithMessage("User Name is required.");
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(p => p.Password).NotEmpty().MinimumLength(8).WithMessage("Password is required. Length must be larger then 7.");
        }
    }
}
