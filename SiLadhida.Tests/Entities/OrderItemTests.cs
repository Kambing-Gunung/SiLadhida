using SiLadhida.Core.Entities;

namespace SiLadhida.Tests.Entities;

public class OrderItemTests
{
    [Fact]
    public void Create_WithValidData_ShouldCreateItem()
    {
        var item =
            OrderItem.Create(1, 2, 10000);

        Assert.Equal(1, item.ProductId);
        Assert.Equal(2, item.Quantity);
        Assert.Equal(10000, item.Harga);
    }

    [Fact]
    public void Create_WithInvalidQuantity_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(
            () => OrderItem.Create(1, 0, 10000));
    }

    [Fact]
    public void IncreaseQuantity_ShouldIncreaseQuantity()
    {
        var item =
            OrderItem.Create(1, 2, 10000);

        item.IncreaseQuantity(3);

        Assert.Equal(5, item.Quantity);
    }

    [Fact]
    public void DecreaseQuantity_ShouldDecreaseQuantity()
    {
        var item =
            OrderItem.Create(1, 5, 10000);

        item.DecreaseQuantity(2);

        Assert.Equal(3, item.Quantity);
    }

    [Fact]
    public void DecreaseQuantity_MoreThanAvailable_ShouldThrow()
    {
        var item =
            OrderItem.Create(1, 5, 10000);

        Assert.Throws<InvalidOperationException>(
            () => item.DecreaseQuantity(10));
    }

    [Fact]
    public void SubTotal_ShouldBeCalculatedCorrectly()
    {
        var item =
            OrderItem.Create(1, 3, 10000);

        Assert.Equal(30000, item.SubTotal);
    }
}