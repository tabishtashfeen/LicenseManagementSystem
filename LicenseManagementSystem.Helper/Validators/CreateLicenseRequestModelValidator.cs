using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Helper.Validators
{
    public class CreateLicenseRequestModelValidator : AbstractValidator<CreateLicenseRequestModel>
    {
        public CreateLicenseRequestModelValidator()
        {
            RuleFor(p => p.ProductId).NotEmpty().WithMessage("Product Id is required.");
            RuleFor(p => p.UserId).NotEmpty().WithMessage("User Id is required.");
        }
    }
}