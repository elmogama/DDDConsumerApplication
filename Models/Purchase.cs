using Npgsql;

namespace ConsumerApplication.Models;

public class Purchase
{
    public Purchase() { }

    public Purchase(NpgsqlDataReader data)
    {
        PurchaseId = data.GetInt32(0);
        OrderId = data.GetInt32(1);
        ProductId = data.GetInt32(2);
        Quantity = data.GetInt32(3);
    }

    public int PurchaseId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}