namespace PingDong.Kata;

public record CartItem
{
    public int SkuId { get; init; }
    public decimal Quantity { get; init; }
}

public record CartPrice
{
    public decimal Total { get; init; }
    public decimal TotalDiscount { get; init; }
    public IEnumerable<CartPriceItem>? Items { get; init; }

    public static CartPrice Empty => new() { Total = 0 };
}

public record CartPriceItem : CartItem
{
    public decimal UnitPrice { get; init; }
    public decimal Amount { get; init; }
    public decimal? AppliedDiscount { get; init; }
    public string? DiscountDescription { get; init; }
}