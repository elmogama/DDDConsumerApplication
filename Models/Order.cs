using Npgsql;

namespace ConsumerApplication.Models;

public class Order
{
    public Order() { }

    public Order(NpgsqlDataReader data)
    {
        OrderId = data.GetInt32(0);
        CreditCardNum = data.GetString(1);
        Status = data.GetString(2);
        DeliveryType = data.GetString(3);
        DeliveryPrice = data.GetDecimal(4);
        OrderDate = data.GetDateTime(5);
        ShipDate = data.GetDateTime(6);
        DeliveryDate = data.GetDateTime(7);
        ShippingAddress = data.GetString(8);
    }

    public int OrderId { get; set; }
    public string CreditCardNum { get; set; }
    public string Status { get; set; }
    public string DeliveryType { get; set; }
    public decimal DeliveryPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string ShippingAddress { get; set; }
}