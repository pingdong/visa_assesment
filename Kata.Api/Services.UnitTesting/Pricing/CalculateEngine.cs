using Xunit;

namespace PingDong.Kata.Service.Testing;

public class CalculateEngineTests
{
    private readonly ICalculateEngine _engine;

    public CalculateEngineTests()
    {
        _engine = new CalculateEngine();
    }

    [Fact]
    public void NotStarted_WithValidInput_ReturnEmpty()
    {
        // Arrange
        var rule = new DiscountRule
        {
            Start = DateTime.UtcNow.AddDays(2),
            Condition = new DiscountCondition
            {
                Type = DiscountConditionType.MultiBuy,
                Value = 3
            },
            Action = new DiscountAction
            {
                Type = DiscountActionType.FixedPrice,
                Value = 1.2m
            }
        };

        // Act
        var actual = _engine.CalculateDiscount(rule, 1, 1);

        // Assert
        Assert.Equal(0, actual);
    }

    [Fact]
    public void Expired_WithValidInput_ReturnEmpty()
    {
        // Arrange
        var rule = new DiscountRule
        {
            Start = DateTime.UtcNow.AddDays(-5),
            End = DateTime.UtcNow.AddDays(-2),
            Condition = new DiscountCondition
            {
                Type = DiscountConditionType.MultiBuy,
                Value = 3
            },
            Action = new DiscountAction
            {
                Type = DiscountActionType.FixedPrice,
                Value = 1.2m
            }
        };

        // Act
        var actual = _engine.CalculateDiscount(rule, 1, 1);

        // Assert
        Assert.Equal(0, actual);
    }

    [Fact]
    public void MultipleBuy_WithoutQuantity_ReturnEmpty()
    {
        // Arrange
        var rule = new DiscountRule
        {
            Condition = new DiscountCondition
            {
                Type = DiscountConditionType.MultiBuy,
                Value = 3
            },
            Action = new DiscountAction
            {
                Type = DiscountActionType.FixedPrice,
                Value = 1.2m
            }
        };

        // Act
        var actual = _engine.CalculateDiscount(rule, 0, 1);

        // Assert
        Assert.Equal(0, actual);
    }

    [Fact]
    public void MultipleBuy_WithoutPrice_ReturnEmpty()
    {
        // Arrange
        var rule = new DiscountRule
        {
            Condition = new DiscountCondition
            {
                Type = DiscountConditionType.MultiBuy,
                Value = 3
            },
            Action = new DiscountAction
            {
                Type = DiscountActionType.FixedPrice,
                Value = 1.2m
            }
        };

        // Act
        var actual = _engine.CalculateDiscount(rule, 1, 0);

        // Assert
        Assert.Equal(0, actual);
    }
    
    [Theory]
    [InlineData(1, 0.5, 0)]
    [InlineData(2, 0.5, 0)]
    [InlineData(3, 0.5, 0.3)]
    [InlineData(4, 0.5, 0.3)]
    [InlineData(5, 0.5, 0.3)]
    [InlineData(6, 0.5, 0.6)]
    [InlineData(7, 0.5, 0.6)]
    public void MultipleBuy_FixedPrice_MultipleRedeeem_ReturnDiscount(decimal quantity, decimal unitPrice, decimal expected)
    {
        // Arrange
        var rule = new DiscountRule
        {
            Condition = new DiscountCondition
            {
                Type = DiscountConditionType.MultiBuy,
                Value = 3
            },
            Action = new DiscountAction
            {
                Type = DiscountActionType.FixedPrice,
                Value = 1.2m
            }
        };

        // Act
        var actual = _engine.CalculateDiscount(rule, quantity, unitPrice);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1, 0.5, 0)]
    [InlineData(2, 0.5, 0)]
    [InlineData(3, 0.5, 0.3)]
    [InlineData(4, 0.5, 0.3)]
    [InlineData(5, 0.5, 0.3)]
    [InlineData(6, 0.5, 0.3)]
    [InlineData(7, 0.5, 0.3)]
    public void MultipleBuy_FixedPrice_SingleRedeem_ReturnDiscount(decimal quantity, decimal unitPrice, decimal expected)
    {
        // Arrange
        var rule = new DiscountRule
        {
            Condition = new DiscountCondition
            {
                Type = DiscountConditionType.MultiBuy,
                MaxRedeems = 1,
                Value = 3
            },
            Action = new DiscountAction
            {
                Type = DiscountActionType.FixedPrice,
                Value = 1.2m
            }
        };

        // Act
        var actual = _engine.CalculateDiscount(rule, quantity, unitPrice);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(1, 0.5, 0)]
    [InlineData(2, 0.5, 0)]
    [InlineData(3, 0.5, 0)]
    [InlineData(4, 0.5, 0.5)]
    [InlineData(5, 0.5, 0.5)]
    [InlineData(6, 0.5, 0.5)]
    [InlineData(7, 0.5, 0.5)]
    [InlineData(8, 0.5, 1.0)]
    [InlineData(9, 0.5, 1.0)]
    public void MultipleBuy_ExtraFree_MultipleRedeem_ReturnDiscount(decimal quantity, decimal unitPrice, decimal expected)
    {
        // Arrange
        var rule = new DiscountRule
        {
            Condition = new DiscountCondition
            {
                Type = DiscountConditionType.MultiBuy,
                Value = 3
            },
            Action = new DiscountAction
            {
                Type = DiscountActionType.ExtraFree,
                Value = 1
            }
        };

        // Act
        var actual = _engine.CalculateDiscount(rule, quantity, unitPrice);

        // Assert
        Assert.Equal(expected, actual);
    }


    [Theory]
    [InlineData(1, 0.5, 0)]
    [InlineData(2, 0.5, 0)]
    [InlineData(3, 0.5, 0)]
    [InlineData(4, 0.5, 0.5)]
    [InlineData(5, 0.5, 0.5)]
    [InlineData(6, 0.5, 0.5)]
    [InlineData(7, 0.5, 0.5)]
    [InlineData(8, 0.5, 0.5)]
    [InlineData(9, 0.5, 0.5)]
    public void MultipleBuy_ExtraFree_SingleRedeem_ReturnDiscount(decimal quantity, decimal unitPrice, decimal expected)
    {
        // Arrange
        var rule = new DiscountRule
        {
            Condition = new DiscountCondition
            {
                Type = DiscountConditionType.MultiBuy,
                MaxRedeems = 1,
                Value = 3
            },
            Action = new DiscountAction
            {
                Type = DiscountActionType.ExtraFree,
                Value = 1
            }
        };

        // Act
        var actual = _engine.CalculateDiscount(rule, quantity, unitPrice);

        // Assert
        Assert.Equal(expected, actual);
    }

}
