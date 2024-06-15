using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Helper.Validators
{
    public class AuthRequestModelValidator : AbstractValidator<AuthRequestModel>
    {
        public AuthRequestModelValidator()
        {
            RuleFor(p => p.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Matches(Constants.EmailRegex).WithMessage("Invalid email format.");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}
