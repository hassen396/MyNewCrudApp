using System.ComponentModel.DataAnnotations;

namespace Crud.Models;
public class User
{
    public int Id { get; set; }
    public string Name { get;  set; }
   
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}