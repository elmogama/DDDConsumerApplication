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
}