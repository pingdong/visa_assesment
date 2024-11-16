using PingDong.Kata.Infrastructure;

namespace PingDong.Kata.Service;

public class PricingService : IPricingService
{
    private readonly ISkuRepository _skuRepository;
    private readonly IRuleRepository _ruleRepository;
    private readonly ICalculateEngine _calculateEngine;

    public PricingService(
        ISkuRepository skuRepository, 
        IRuleRepository ruleRepository, 
        ICalculateEngine calculateEngine
    ) {
        _skuRepository = skuRepository;
        _ruleRepository = ruleRepository;
        _calculateEngine = calculateEngine;
    }

    public async Task<CartPrice> CalculateAsync(IEnumerable<CartItem> items)
    {
        if (!items.Any()) return CartPrice.Empty;

        // Get all SKUs
        var skuIds = items.Select(i => i.SkuId);
        var skus = await _skuRepository.GetByIdsAsync(skuIds);
        var rules = await _ruleRepository.GetByIdsAsync(skuIds);

        if (skus == null || !skus.Any() ||
            rules == null || !rules.Any()) 
            return CartPrice.Empty;

        // Prepare the data for calculation
        var shoppingCart = from item in items
                           group item by item.SkuId into product
                           select new
                           {
                               Sku = skus.FirstOrDefault(s => s.Id == product.Key),
                               Rule = rules.FirstOrDefault(r => r.Condition.SkuId == product.Key),
                               Quantity = product.Sum(p => p.Quantity)
                           };

        // Calculate the price
        var prices = new List<CartPriceItem>();
        foreach (var item in shoppingCart)
        {
            var discount = 0M;
            if (item.Rule != null && item.Sku != null)
            {
                discount = _calculateEngine.CalculateDiscount(item.Rule, item.Quantity, item.Sku.UnitPrice);
            }

            if (item.Sku != null)
            {

                var priceItem = new CartPriceItem
                {
                    SkuId = item.Sku.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.Sku.UnitPrice,
                    Amount = item.Quantity * item.Sku.UnitPrice - discount,
                    AppliedDiscount = item.Rule != null && discount > 0 ? discount : null,
                    DiscountDescription = item.Rule != null && discount > 0 ? item.Rule.Description : null
                };

                prices.Add(priceItem);
            }
        }

        return new CartPrice
        {
            Total = prices.Sum(p => p.Amount),
            TotalDiscount = prices.Sum(p => p.AppliedDiscount ?? 0),
            Items = prices
        };
    }
}
