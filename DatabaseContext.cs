using ConsumerApplication.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ConsumerApplication.Data;

public class DatabaseContext : DbContext
{
    protected readonly IConfiguration _config;

    public DatabaseContext(IConfiguration config)
    {
        _config = config;
    }

    public List<T> ExecuteSelect<T>(string sql)
    {
        List<T> rows = new List<T>();

        using (NpgsqlConnection conn = new NpgsqlConnection(_config.GetConnectionString("Default")))
        {
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
            NpgsqlDataReader datarow = comm.ExecuteReader();
            while (datarow.Read())
            {
                T row = (T)Activator.CreateInstance(typeof(T), new object[] { datarow });
                rows.Add(row);
            }

            return rows;
        }
    }

    public void ExecuteUpdate<T>(string sql)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(_config.GetConnectionString("Default")))
        {
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
            NpgsqlDataReader datarow = comm.ExecuteReader();
        }
    }
}