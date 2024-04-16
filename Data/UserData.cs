using ConsumerApplication.Models;

namespace ConsumerApplication.Data.Data;

public class UserData
{

    private DatabaseContext _db;
    
    public UserData(DatabaseContext db)
    {
        _db = db;
    }
    
    public users GetUser(users inputUser)
    {
        return this._db.users.First(x => x.userid == inputUser.userid);
    }
}