using SiLadhida.Core.Entities;
using SiLadhida.Core.Enums;

namespace SiLadhida.Tests.Entities;

public class OrderTests
{
    [Fact]
    public void Create_ShouldInitializeStatus()
    {
        var order =
            Order.Create("David");

        Assert.Equal(
            StateOrder.MenungguPembayaran,
            order.StatusSekarang);
    }

    [Fact]
    public void AddItem_ShouldAddItem()
    {
        var order =
            Order.Create("David");

        order.AddItem(1, 2, 10000);

        Assert.Single(order.Items);
    }

    [Fact]
    public void AddItem_SameProduct_ShouldMergeQuantity()
    {
        var order =
            Order.Create("David");

        order.AddItem(1, 2, 10000);
        order.AddItem(1, 3, 10000);

        Assert.Single(order.Items);
        Assert.Equal(5, order.Items[0].Quantity);
    }

    [Fact]
    public void RemoveItem_ShouldRemoveItem()
    {
        var order =
            Order.Create("David");

        order.AddItem(1, 2, 10000);

        order.RemoveItem(1);

        Assert.Empty(order.Items);
    }

    [Fact]
    public void ClearItems_ShouldRemoveAllItems()
    {
        var order =
            Order.Create("David");

        order.AddItem(1, 2, 10000);
        order.AddItem(2, 3, 5000);

        order.ClearItems();

        Assert.Empty(order.Items);
    }

    [Fact]
    public void IncreaseItemQuantity_ShouldIncreaseQuantity()
    {
        var order =
            Order.Create("David");

        order.AddItem(1, 2, 10000);

        order.IncreaseItemQuantity(1, 3);

        Assert.Equal(5,
            order.Items[0].Quantity);
    }

    [Fact]
    public void DecreaseItemQuantity_ShouldDecreaseQuantity()
    {
        var order =
            Order.Create("David");

        order.AddItem(1, 5, 10000);

        order.DecreaseItemQuantity(1, 2);

        Assert.Equal(3,
            order.Items[0].Quantity);
    }

    [Fact]
    public void DecreaseItemQuantity_ToZero_ShouldRemoveItem()
    {
        var order =
            Order.Create("David");

        order.AddItem(1, 1, 10000);

        order.DecreaseItemQuantity(1, 1);

        Assert.Empty(order.Items);
    }

    [Fact]
    public void TotalHarga_ShouldCalculateCorrectly()
    {
        var order =
            Order.Create("David");

        order.AddItem(1, 2, 10000);
        order.AddItem(2, 1, 5000);

        Assert.Equal(25000,
            order.TotalHarga);
    }

    [Fact]
    public void Pay_ShouldChangeStatus()
    {
        var order =
            Order.Create("David");

        order.AddItem(1, 1, 10000);

        order.Pay();

        Assert.Equal(
            StateOrder.SiapDiambil,
            order.StatusSekarang);
    }

    [Fact]
    public void Cancel_ShouldChangeStatus()
    {
        var order =
            Order.Create("David");

        order.Cancel();

        Assert.Equal(
            StateOrder.Dibatalkan,
            order.StatusSekarang);
    }

    [Fact]
    public void Complete_ShouldChangeStatus()
    {
        var order =
            Order.Create("David");

        order.AddItem(1, 1, 10000);

        order.Pay();
        order.Complete();

        Assert.Equal(
            StateOrder.Selesai,
            order.StatusSekarang);
    }
}