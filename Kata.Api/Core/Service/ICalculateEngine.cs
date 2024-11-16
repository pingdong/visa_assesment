namespace PingDong.Kata.Service;

public interface ICalculateEngine
{
    decimal CalculateDiscount(DiscountRule rule, decimal quantity, decimal unitPrice);
}