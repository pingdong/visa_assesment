using FluentValidation;

namespace PingDong.Kata.Request;

public record PriceRequest
{
    public required IEnumerable<CartItem> Items { get; init; }
}

public class PriceRequestValidator : AbstractValidator<PriceRequest>
{
    public PriceRequestValidator()
    {
        RuleFor(x => x.Items)
            .NotNull()
            .NotEmpty()
            .WithMessage("Items is required");
        RuleForEach(x => x.Items)
            .SetValidator(new PriceRequestItemValidator());
    }
}

public class PriceRequestItemValidator : AbstractValidator<CartItem>
{
    public PriceRequestItemValidator()
    {
        RuleFor(x => x.SkuId)
            .NotNull()
            .NotEmpty()
            .WithMessage("ProductId is required");
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
    }
}