using SiLadhida.Core.Entities;

namespace SiLadhida.Tests.Entities;

public class ProductTests
{
    [Fact]
    public void Create_WithValidData_ShouldCreateProduct()
    {
        var product =
            Product.Create("Brownies", 25000, 10);

        Assert.Equal("Brownies", product.Nama);
        Assert.Equal(25000, product.Harga);
        Assert.Equal(10, product.Stock);
    }

    [Fact]
    public void Create_WithEmptyName_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(
            () => Product.Create("", 25000, 10));
    }

    [Fact]
    public void Create_WithNegativePrice_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(
            () => Product.Create("Brownies", -1, 10));
    }

    [Fact]
    public void Create_WithNegativeStock_ShouldThrow()
    {
        Assert.Throws<ArgumentException>(
            () => Product.Create("Brownies", 25000, -1));
    }

    [Fact]
    public void AddStock_ShouldIncreaseStock()
    {
        var product =
            Product.Create("Brownies", 25000, 10);

        product.IncreaseStock(5);

        Assert.Equal(15, product.Stock);
    }

    [Fact]
    public void DecreaseStock_ShouldReduceStock()
    {
        var product =
            Product.Create("Brownies", 25000, 10);

        product.DecreaseStock(4);

        Assert.Equal(6, product.Stock);
    }

    [Fact]
    public void DecreaseStock_TooMuch_ShouldThrow()
    {
        var product =
            Product.Create("Brownies", 25000, 10);

        Assert.Throws<InvalidOperationException>(
            () => product.DecreaseStock(20));
    }
}