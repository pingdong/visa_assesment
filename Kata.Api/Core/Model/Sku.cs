namespace PingDong.Kata;

public record Product
{
    public int Id { get; init; }
    public string Name { get; init; }
}

public record Sku
{
    public int Id { get; init; }
    public string Name { get; init; }
    public IEnumerable<SkuItem> Items { get; init; }
    public decimal UnitPrice { get; init; }
}

public record SkuItem
{
    public int ProductId { get; init; }
    public decimal Quantity { get; init; }
}
