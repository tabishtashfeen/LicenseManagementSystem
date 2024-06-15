namespace LicenseManagementSystem.Helper.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductRequestModel>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(p => p.Version).NotEmpty().WithMessage("Product version is required.");
        }
    }
}
