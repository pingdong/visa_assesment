namespace PingDong.Kata.Infrastructure;

public interface ISkuRepository
{
    Task<IEnumerable<Sku>> GetByIdsAsync(IEnumerable<int> skuIds);

    Task<IEnumerable<Sku>> GetAllAsync();
}