using Npgsql;

namespace ConsumerApplication.Models;

public class CreditCard
{
    public CreditCard() { }

    public CreditCard(NpgsqlDataReader data)
    {
        CreditCardNum = data.GetString(0);
        CustomerId = data.GetInt32(1);
        BillingAddress = data.GetString(2);
    }

    public string CreditCardNum { get; set; }
    public int CustomerId { get; set; }
    public string BillingAddress { get; set; }
}