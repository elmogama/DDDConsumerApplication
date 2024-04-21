using Npgsql;

namespace ConsumerApplication.Models;

public class ProductWithStock
{
    
    public ProductWithStock(){}
    
    public ProductWithStock(NpgsqlDataReader data)
    {
        ProductId = data.GetInt32(0);
        ProductName= data.GetString(1);
        Type = data.GetString(2);
        Price = data.GetDecimal(3);
        Brand = data.GetString(4);
        Description = data.GetString(5);
        ImageLink = data.GetString(6);
        Size = data.GetString(7);
        Stock = data.GetInt32(8);
    }
    
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }
    public string Brand { get; set; }
    public string Description { get; set; }
    public string ImageLink { get; set; }
    public string Size { get; set; }
    public int Stock { get; set; }
    
}