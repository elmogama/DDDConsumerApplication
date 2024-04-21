using ConsumerApplication.Models;

namespace ConsumerApplication.Data.Data;

public class EmployeeData
{
    private DatabaseContext _db;
    
    public EmployeeData(DatabaseContext db)
    {
        _db = db;
    }

    public bool RegisterEmployee(Employee inputUser)
    {
        try
        {
            _db.ExecuteUpdate<Employee>(
                $"INSERT INTO employees (userid, name, jobtitle, salary, emp_address) VALUES ({inputUser.UserId}, '{inputUser.Name}', '{inputUser.JobTitle}', {inputUser.Salary}, '{inputUser.Emp_Address}')");
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}