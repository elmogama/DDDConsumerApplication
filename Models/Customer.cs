using Npgsql;

namespace ConsumerApplication.Models;

public class Customer
{
    public Customer() { }

    public Customer(NpgsqlDataReader data)
    {
        CustomerId = data.GetInt32(0);
        UserId = data.GetInt32(1);
        Name = data.GetString(2);
        ShippingAddresses = (List<string>)data["ShippingAddresses"];
        OutstandingBalance = data.GetDecimal(4);
    }

    public int CustomerId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public List<string> ShippingAddresses { get; set; }
    public decimal OutstandingBalance { get; set; }
}