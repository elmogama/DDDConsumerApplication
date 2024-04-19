using Npgsql;

namespace ConsumerApplication.Models;

public class ProductWarehouseMapping
{
    public ProductWarehouseMapping() { }

    public ProductWarehouseMapping(NpgsqlDataReader data)
    {
        ProductId = data.GetInt32(0);
        WarehouseId = data.GetInt32(1);
        Stock = data.GetInt32(2);
    }

    public int ProductId { get; set; }
    public int WarehouseId { get; set; }
    public int Stock { get; set; }
}