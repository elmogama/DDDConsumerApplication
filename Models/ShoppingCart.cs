using Npgsql;

namespace ConsumerApplication.Models;

public class ShoppingCart
{
    public ShoppingCart(NpgsqlDataReader data)
    {
        ProductId = data.GetInt32(0);
        ImageLink = data.GetString(1);
        ProductName = data.GetString(2);
        Quantity = data.GetInt32(3);
        Price = data.GetDecimal(4);
        TotalPrice = data.GetDecimal(5);
    }

    public int ProductId { get; set; }
    public string ImageLink { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}