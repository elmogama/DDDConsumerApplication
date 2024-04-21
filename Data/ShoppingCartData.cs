using ConsumerApplication.Models;

namespace ConsumerApplication.Data.Data;

public class ShoppingCartData
{
    private DatabaseContext _db;
    
    public ShoppingCartData(DatabaseContext db)
    {
        _db = db;
    }

    public List<ShoppingCart> GetShoppingCartItems(string customerId)
    {
        string sql = $"SELECT prod.productid, prod.imagelink, prod.productname, p.quantity, prod.price, (prod.price * p.quantity) as totalprice FROM purchases AS p " +
                     $"LEFT JOIN orders AS o ON p.orderid = o.orderid " +
                     $"LEFT JOIN creditcards AS cc ON cc.creditcardnum = o.creditcardnum " +
                     $"LEFT JOIN customers c ON c.customerid = cc.customerid " +
                     $"LEFT JOIN products AS prod ON prod.productid = p.productid " +
                     $"WHERE lower(status) = lower('In Cart') AND c.customerid = {customerId}";
        return _db.ExecuteSelect<ShoppingCart>(sql);
    }

    public void RemoveShoppingCartItem(int productid, string customerId)
    {
        string sql = $"DELETE FROM purchases WHERE " +
                     $"orderid = (SELECT o.orderid FROM purchases AS p " +
                     $"LEFT JOIN orders AS o ON p.orderid = o.orderid " +
                     $"LEFT JOIN creditcards AS cc ON cc.creditcardnum = o.creditcardnum " +
                     $"LEFT JOIN customers c ON c.customerid = cc.customerid " +
                     $"LEFT JOIN products AS prod ON prod.productid = p.productid " +
                     $"WHERE lower(status) = lower('In Cart') AND c.customerid = {customerId} AND p.productid = {productid}) AND productid = {productid}";
        _db.ExecuteUpdate<ShoppingCart>(sql);
    }

    public void ChangeQuantity(int productid, string quantity, string customerId)
    {
        string sql = $"UPDATE purchases SET quantity = {quantity} WHERE " +
                     $"orderid = (SELECT o.orderid FROM purchases AS p " +
                     $"LEFT JOIN orders AS o ON p.orderid = o.orderid " +
                     $"LEFT JOIN creditcards AS cc ON cc.creditcardnum = o.creditcardnum " +
                     $"LEFT JOIN customers c ON c.customerid = cc.customerid " +
                     $"LEFT JOIN products AS prod ON prod.productid = p.productid " +
                     $"WHERE lower(status) = lower('In Cart') AND c.customerid = {customerId} AND p.productid = {productid}) AND productid = {productid}";
        _db.ExecuteUpdate<ShoppingCart>(sql);
    }

    public void Checkout(string customerId)
    {
        string sql = $"UPDATE orders SET status = 'Shipped' WHERE " +
                     $"orderid = (SELECT o.orderid FROM purchases AS p " +
                     $"LEFT JOIN orders AS o ON p.orderid = o.orderid " +
                     $"LEFT JOIN creditcards AS cc ON cc.creditcardnum = o.creditcardnum " +
                     $"LEFT JOIN customers c ON c.customerid = cc.customerid " +
                     $"LEFT JOIN products AS prod ON prod.productid = p.productid " +
                     $"WHERE lower(status) = lower('In Cart') AND c.customerid = {customerId})";
        _db.ExecuteUpdate<ShoppingCart>(sql);
    }

    public void ChangeDeliveryType(string deliveryType, decimal deliveryPrice, string customerId)
    {
        string sql = $"UPDATE orders SET deliverytype = '{deliveryType}', deliveryPrice = {deliveryPrice} WHERE " +
                     $"orderid = (SELECT o.orderid FROM purchases AS p " +
                     $"LEFT JOIN orders AS o ON p.orderid = o.orderid " +
                     $"LEFT JOIN creditcards AS cc ON cc.creditcardnum = o.creditcardnum " +
                     $"LEFT JOIN customers c ON c.customerid = cc.customerid " +
                     $"LEFT JOIN products AS prod ON prod.productid = p.productid " +
                     $"WHERE lower(status) = lower('In Cart') AND c.customerid = {customerId})";
        _db.ExecuteUpdate<ShoppingCart>(sql);
    }

    public Order GetOrder(string customerId)
    {
        string sql = $"SELECT o.* FROM purchases AS p " +
                  $"LEFT JOIN orders AS o ON p.orderid = o.orderid " +
                  $"LEFT JOIN creditcards AS cc ON cc.creditcardnum = o.creditcardnum " +
                  $"LEFT JOIN customers c ON c.customerid = cc.customerid " +
                  $"LEFT JOIN products AS prod ON prod.productid = p.productid " +
                  $"WHERE lower(status) = lower('In Cart') AND c.customerid = {customerId}";
        return _db.ExecuteSelect<Order>(sql)[0];
    }
}