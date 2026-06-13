using SiLadhida.Core.Enums;
using SiLadhida.Core.StateMachines;
using SiLadhida.Core.Validators;

namespace SiLadhida.Core.Entities;

public class Order
{
    public int Id { get; private set; }
    public string NamaPemesan { get; private set; }
    public StateOrder StatusSekarang { get; private set; }

    public List<OrderItem> Items { get; private set; } = new();

    public decimal TotalHarga => Items.Sum(x => x.SubTotal);

    private Order(string namaPemesan)
    {
        NamaPemesan = namaPemesan.Trim();
        StatusSekarang = StateOrder.MenungguPembayaran;
    }

    public static Order Create(string namaPemesan)
    {
        OrderValidator.ValidateNamaPemesan(namaPemesan);

        return new Order(namaPemesan);
    }

    public void Rename(string namaBaru)
    {
        OrderValidator.ValidateNamaPemesan(namaBaru);

        NamaPemesan = namaBaru.Trim();
    }

    public void AddItem(int productId, int quantity, decimal harga)
    {
        EnsureEditable();

        var existing = Items.FirstOrDefault(x => x.ProductId == productId);

        if (existing is not null)
        {
            existing.IncreaseQuantity(quantity);
            return;
        }

        Items.Add(OrderItem.Create(productId, quantity, harga));
    }

    public void RemoveItem(int productId)
    {
        EnsureEditable();

        var item = Items.FirstOrDefault(x => x.ProductId == productId) ??
            throw new InvalidOperationException("Item tidak ditemukan.");
            
        Items.Remove(item);
    }

    public void ClearItems()
    {
        EnsureEditable();

        Items.Clear();
    }

    public void IncreaseItemQuantity(int productId, int quantity)
    {
        EnsureEditable();

        var item = Items.FirstOrDefault(x => x.ProductId == productId) ??
            throw new InvalidOperationException("Item tidak ditemukan.");

        item.IncreaseQuantity(quantity);
    }

    public void DecreaseItemQuantity(int productId, int quantity)
    {
        EnsureEditable();

        var item = Items.FirstOrDefault(x => x.ProductId == productId) ??
            throw new InvalidOperationException("Item tidak ditemukan.");

        item.DecreaseQuantity(quantity);

        if (item.Quantity == 0)
            Items.Remove(item);
    }

    public void Pay()
    {
        if (!Items.Any())
            throw new InvalidOperationException("Order harus memiliki minimal satu item.");

        Transition(StateTrigger.PembayaranDikonfirmasi);
    }

    public void Cancel()
    {
        Transition(StateTrigger.DibatalkanPelanggan);
    }

    public void Complete()
    {
        Transition(StateTrigger.KueDiambilPelanggan);
    }

    private void EnsureEditable()
    {
        if (StatusSekarang != StateOrder.MenungguPembayaran)
            throw new InvalidOperationException(
                "Order tidak dapat diubah karena sudah diproses.");
    }

    private void Transition(StateTrigger trigger)
    {
        if (!OrderStateMachine.CanTransition(StatusSekarang, trigger))
            throw new InvalidOperationException("Transisi status tidak valid.");

        StatusSekarang = OrderStateMachine.GetNext(StatusSekarang, trigger);
    }
}