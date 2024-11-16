namespace PingDong.Kata.Service;

public class CalculateEngine : ICalculateEngine
{
    public decimal CalculateDiscount(DiscountRule rule, decimal quantity, decimal unitPrice)
    {
        if (rule.Start > DateTime.Now) return 0;
        if (rule.End != null && rule.End <= DateTime.Now) return 0;
        if (quantity == 0 || unitPrice == 0) return 0;

        switch (rule.Condition.Type)
        {
            case DiscountConditionType.MultiBuy:
                return CalculateMultiBuyDiscount(rule.Condition, quantity, unitPrice, rule.Action);
            default:
                return 0;
        }
    }

    private decimal CalculateMultiBuyDiscount(DiscountCondition condition, decimal quantity, decimal unitPrice, DiscountAction action)
    {
        var appliedTimes = 0;
        switch (action.Type)
        {
            case DiscountActionType.FixedPrice:
                appliedTimes = Convert.ToInt32(Math.Floor(quantity / condition.Value));
                break;
            case DiscountActionType.ExtraFree:
                appliedTimes = Convert.ToInt32(Math.Floor(quantity / (condition.Value + action.Value)));
                break;
            default:
                return 0;
        }

        if (appliedTimes == 0) return 0;

        var redeemTimes = condition.MaxRedeems != null
            ? Math.Min(condition.MaxRedeems.Value, appliedTimes)
            : appliedTimes;

        switch (action.Type)
        {
            case DiscountActionType.FixedPrice:
                return (unitPrice * condition.Value - action.Value) * redeemTimes;
            case DiscountActionType.ExtraFree:
                return unitPrice * action.Value * redeemTimes;
            default:
                return 0;
        }
    }
}
