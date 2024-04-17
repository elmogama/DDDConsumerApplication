
using Npgsql;

namespace ConsumerApplication.Models;

public class User
{

    public User(){}
    
    public User(NpgsqlDataReader data)
    {
        Userid = data.GetInt32(0);
        Usertype = data.GetString(1);
        Password = data.GetString(2);
        Email = data.GetString(3);
    }
    
    public int Userid { get; set; }
    public string Usertype { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}