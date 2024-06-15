namespace LicenseManagementSystem.Helper.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequestModel>
    {
        public UpdateProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(p => p.Version).NotEmpty().WithMessage("Product version is required.");
        }
    }
}
