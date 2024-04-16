using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ConsumerApplication.Models;

[PrimaryKey("userid")]
public class users
{
    public int userid { get; set; }
    [Required]
    public string usertype { get; set; }
    public string email { get; set; }
    public string password { get; set; }
}