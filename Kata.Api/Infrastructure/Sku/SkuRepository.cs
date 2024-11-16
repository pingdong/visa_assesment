
namespace PingDong.Kata.Infrastructure;

public class SkuRepository : ISkuRepository
{
    private static readonly List<Sku> Skus =
    [
        new Sku
        {
            Id = 1,
            UnitPrice = 0.5m,
            Items = new List<SkuItem>
            {
                new SkuItem
                {
                    ProductId = 1,
                    Quantity = 1
                }
            }
        },

        new Sku
        {
            Id = 2,
            UnitPrice = 1m,
            Items = new List<SkuItem>
            {
                new SkuItem
                {
                    ProductId = 2,
                    Quantity = 1
                }
            }
        }
    ];

    public Task<IEnumerable<Sku>> GetByIdsAsync(IEnumerable<int> skuIds)
    {
        var skus = Skus.Where(s => skuIds.Contains(s.Id));

        return Task.FromResult(skus);
    }

    public Task<IEnumerable<Sku>> GetAllAsync()
    {
        return Task.FromResult(Skus.AsEnumerable());
    }
}
