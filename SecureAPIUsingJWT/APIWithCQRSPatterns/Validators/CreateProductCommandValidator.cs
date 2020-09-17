using APIWithCQRSPatterns.Features.ProductFeatures.Commands;
using FluentValidation;

namespace APIWithCQRSWithMediatRPatterns.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(c => c.Barcode).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
