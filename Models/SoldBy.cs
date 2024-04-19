using Npgsql;

namespace ConsumerApplication.Models;

public class SoldBy
{
    public SoldBy() { }

    public SoldBy(NpgsqlDataReader data)
    {
        ProductId = data.GetInt32(0);
        SupplierId = data.GetInt32(1);
        WholesalePrice = data.GetDecimal(2);
    }

    public int ProductId { get; set; }
    public int SupplierId { get; set; }
    public decimal WholesalePrice { get; set; }
}