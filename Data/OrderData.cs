using ConsumerApplication.Models;

namespace ConsumerApplication.Data.Data;

public class OrderData
{
    private DatabaseContext _db;
    
    public OrderData(DatabaseContext db)
    {
        _db = db;
    }


    public List<Order> GetOrders(string customerId)
    {
        string sql = $"SELECT o.* FROM orders AS o " +
                  $"LEFT JOIN creditcards AS cc ON cc.creditcardnum = o.creditcardnum " +
                  $"LEFT JOIN customers c ON c.customerid = cc.customerid " +
                  $"WHERE lower(status) != lower('In Cart') AND c.customerid = {customerId}";

        return _db.ExecuteSelect<Order>(sql);
    }
}