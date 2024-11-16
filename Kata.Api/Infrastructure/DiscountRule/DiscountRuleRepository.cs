namespace PingDong.Kata.Infrastructure;

public class DiscountRuleRepository : IRuleRepository
{
    private static readonly List<DiscountRule> Rules =
    [
        new DiscountRule
        {
            Id = 1,
            Description = "Buy 3 from $1.3",
            Start = DateTime.UtcNow.AddDays(-1),
            Condition = new DiscountCondition
            {
                SkuId = 1,
                Type = DiscountConditionType.MultiBuy,
                Value = 3
            },
            Action = new DiscountAction
            {
                Type = DiscountActionType.FixedPrice,
                Value = 1.3m
            }
        },
    ];

    public async Task<IEnumerable<DiscountRule>> GetByIdsAsync(IEnumerable<int> skuIds)
    {
        var rules = Rules.Where(s => skuIds.Contains(s.Id));
        
        return await Task.FromResult(rules);
    }
}
