using Npgsql;

namespace ConsumerApplication.Models
{
    public class Warehouse
    {
        public Warehouse() { }

        public Warehouse(NpgsqlDataReader data)
        {
            WarehouseId = data.GetInt32(0);
            Name = data.GetString(1);
            War_Address = data.GetString(2);
            TotalCapacity = data.GetInt32(3);
            AvailableCapacity = data.GetInt32(4);
        }

        public int WarehouseId { get; set; }
        public string Name { get; set; }
        public string War_Address { get; set; }
        public int TotalCapacity { get; set; }
        public int AvailableCapacity { get; set; }
    }
}