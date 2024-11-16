namespace PingDong.Kata.Service;

public interface IPricingService
{
    Task<CartPrice> CalculateAsync(IEnumerable<CartItem> items);
}