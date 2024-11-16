namespace PingDong.Kata.Infrastructure;

public interface IRuleRepository
{
    Task<IEnumerable<DiscountRule>> GetByIdsAsync(IEnumerable<int> skuIds);
}