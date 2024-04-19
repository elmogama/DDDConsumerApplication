using Npgsql;

namespace ConsumerApplication.Models;

public class Employee
{
    public Employee() { }

    public Employee(NpgsqlDataReader data)
    {
        EmployeeId = data.GetInt32(0);
        UserId = data.GetInt32(1);
        Name = data.GetString(2);
        JobTitle = data.GetString(3);
        Salary = data.GetInt32(4);
        Emp_Address = data.GetString(5);
    }

    public int EmployeeId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string JobTitle { get; set; }
    public int Salary { get; set; }
    public string Emp_Address { get; set; }
}