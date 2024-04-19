using Npgsql;

namespace ConsumerApplication.Models;

public class Supplier
{
    public Supplier() { }

    public Supplier(NpgsqlDataReader data)
    {
        SupplierId = data.GetInt32(0);
        Name = data.GetString(1);
        Sup_Address = data.GetString(2);
    }

    public int SupplierId { get; set; }
    public string Name { get; set; }
    public string Sup_Address { get; set; }
}