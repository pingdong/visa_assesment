namespace PingDong.Kata;

public record DiscountRule
{
    public int Id { get; init; }
    public string Description { get; init; }
    public DateTime Start { get; init; }
    public DateTime? End { get; init; }
    public DiscountCondition Condition { get; init; }
    public DiscountAction Action { get; init; }
}

public record DiscountCondition
{
    public int SkuId { get; init; }
    public DiscountConditionType Type { get; init; }
    public decimal Value { get; init; }
    public int? MaxRedeems { get; init; }
}

public enum DiscountConditionType
{
    None,
    MultiBuy
}

public record DiscountAction
{
    public DiscountActionType Type { get; init; }
    public decimal Value { get; init; }
}

public enum DiscountActionType
{
    None,
    FixedPrice,
    ExtraFree
}