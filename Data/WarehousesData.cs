using ConsumerApplication.Components.Pages.Employee;
using ConsumerApplication.Models;

namespace ConsumerApplication.Data.Data;

public class WarehousesData
{

    public DatabaseContext _db;
    
    public WarehousesData(DatabaseContext db)
    {
        _db = db;
    }


    public List<Warehouse> GetAllWarehouses()
    {
        string sql = $"SELECT * FROM warehouses";
        return _db.ExecuteSelect<Warehouse>(sql);
    }
}