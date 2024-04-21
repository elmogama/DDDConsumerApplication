using ConsumerApplication.Models;

namespace ConsumerApplication.Data.Data;

public class CreditCardData
{
    private DatabaseContext _db;
    
    public CreditCardData(DatabaseContext db)
    {
        _db = db;
    }

    public List<CreditCard> GetCreditCardList(string customerId)
    {
        string sql = $"SELECT * FROM creditcards WHERE customerid = {customerId}";
        return _db.ExecuteSelect<CreditCard>(sql);
    }

    public void SaveNewCard(CreditCard creditCard)
    {
        
    }
}