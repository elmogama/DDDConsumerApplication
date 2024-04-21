using ConsumerApplication.Models;

namespace ConsumerApplication.Data.Data;

public class CustomerData
{
    
    private DatabaseContext _db;
    
    public CustomerData(DatabaseContext db)
    {
        _db = db;
    }

    public Customer GetCustomer(int userid)
    {
        string sql = $"SELECT customerid, userid, name, array_to_string(shippingaddresses, ',') AS shippingaddresses, outstandingbalance FROM customers WHERE userid = {userid}";
        return _db.ExecuteSelect<Customer>(sql)[0];
    }

    public bool RegisterCustomer(Customer customer)
    {
        try
        {
            _db.ExecuteUpdate<Customer>(
                $"INSERT INTO customers (userid, name, shippingaddresses) VALUES ({customer.UserId}, '{customer.Name}', '{{{string.Join(",", customer.ShippingAddresses)}}}')");
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}