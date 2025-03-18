using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Commands.Validators
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(c => c.SaleNumber)
                .NotEmpty().WithMessage("SaleNumber é obrigatório.")
                .MaximumLength(50).WithMessage("SaleNumber deve ter no máximo 50 caracteres.");

            RuleFor(c => c.TotalAmount)
                .GreaterThan(0).WithMessage("TotalAmount deve ser maior que zero.");

            RuleFor(c => c.Customer)
                .NotEmpty().WithMessage("Customer é obrigatório.");

            RuleFor(c => c.SaleDate)
                .NotNull().WithMessage("SaleDate é obrigatório.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("SaleDate não pode ser no futuro.");
        }
    }
}
