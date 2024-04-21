using ConsumerApplication.Models;
using Npgsql;

namespace ConsumerApplication.Data.Data;

public class UserData
{
    private DatabaseContext _db;
    
    public UserData(DatabaseContext db)
    {
        _db = db;
    }
    
    public User GetUser(User inputUser)
    {
        List<User> users = _db.ExecuteSelect<User>($"SELECT * FROM users WHERE userid = {inputUser.Userid}");
        User user = users.Count != 0 ? users[0] : null;
        
        return user;
    }

    public User Login(string email, string password)
    {
        List<User> users = _db.ExecuteSelect<User>($"SELECT * FROM users WHERE lower(email) = lower('{email}') AND password = '{password}'");
        User user = users.Count != 0 ? users[0] : null;
        
        return user;
    }

    public void CreateNewUser(User user)
    {
        string sql = $"INSERT INTO users (email, password, usertype) VALUES ('{user.Email}', '{user.Password}', '{user.Usertype}')";
        _db.ExecuteUpdate<User>(sql);
        
    }

    public bool EmailAlreadyUsed(string email)
    {
        List<User> users = _db.ExecuteSelect<User>($"SELECT * FROM users WHERE lower(email) = lower('{email}')");
        return users.Count > 0;
    }
}