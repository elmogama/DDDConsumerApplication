using ConsumerApplication.Models;

namespace ConsumerApplication.Data.Data;

public class ProductData
{
    private DatabaseContext _db;
    
    public ProductData(DatabaseContext db)
    {
        _db = db;
    }
    
    public List<Product> GetProducts(string productName)
    {
        return _db.ExecuteSelect<Product>($"SELECT * FROM products WHERE lower(productname) LIKE lower('%{productName}%')");
    }
    public Product GetProduct(int productId)
    {
        return _db.ExecuteSelect<Product>($"SELECT * FROM products WHERE productid = {productId}")[0];
    }

    public void UpdateProduct(Product product)
    {
        string sql = $"UPDATE products SET productname = '{product.ProductName}', type = '{product.Type}', price = {product.Price}, brand = '{product.Brand}', description = '{product.Description}', imagelink = '{product.ImageLink}', size = '{product.Size}' WHERE productid = {product.ProductId}";
        _db.ExecuteUpdate<Product>(sql);
    }

    public void AddProduct(Product product)
    {
        string sql = $"INSERT INTO products (productname, type, price, brand, description, imagelink, size) VALUES ('{product.ProductName}', '{product.Type}', '{product.Price}', '{product.Brand}', '{product.Description}', '{product.ImageLink}', '{product.Size}')";
        _db.ExecuteUpdate<Product>(sql);
    }

    public List<ProductWithStock> GetProductsForWarehouse(Warehouse warehouse)
    {
        string sql = $"SELECT p.*, pwm.stock " +
                     $"FROM productwarehousemapping AS pwm " + 
                     $"LEFT JOIN products AS p ON p.productid = pwm.productid " +
                     $"WHERE pwm.warehouseid = {warehouse.WarehouseId}";
        return _db.ExecuteSelect<ProductWithStock>(sql);
    }
}