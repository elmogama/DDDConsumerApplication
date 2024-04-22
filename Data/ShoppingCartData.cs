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
    
    public void AddShoppingCardItems(Customer shopper, int productid, int quantity){
        List<ShoppingCart> currentCart = GetShoppingCartItems(shopper.CustomerId.ToString());

        CreditCardData ccd = new CreditCardData(_db);
        string credit = ccd.GetCreditCardList(shopper.CustomerId.ToString())[0].ToString();
        
        if(currentCart.Count == 0){
            string sql = $"INSERT INTO order (CreditCardNum, Status, DeliveryType, DeliveryPrice, OrderDate, ShipDate, DeliveryDate, ShippingAddresses) VALUES ('{credit}', 'In Cart', '', '0', '0001-01-01T00:00:00', '0001-01-01T00:00:00')";

            _db.ExecuteUpdate<Order>(sql);
        }

        int orderID = GetInCartOrders(shopper.CustomerId.ToString()).OrderId;

        string sql2 = $"INSERT INTO purchases ({orderID}, {productid}, {quantity})";
        _db.ExecuteUpdate<Purchase>(sql2);
    }
    public Order GetInCartOrders(string customerId)
    {
        string sql = $"SELECT o.* FROM orders AS o " +
                     $"LEFT JOIN creditcards AS cc ON cc.creditcardnum = o.creditcardnum " +
                     $"LEFT JOIN customers c ON c.customerid = cc.customerid " +
                     $"WHERE lower(status) = lower('In Cart') AND c.customerid = {customerId}";

        return _db.ExecuteSelect<Order>(sql)[0];
    }
    
}