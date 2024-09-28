using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models;

public class User
{
    [Key]
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Name cannot be empty.")]
    public string Name { get; set; } = "";

    public DateTime RegistrationDate { get; set; }
    
    [Required(ErrorMessage = "Password cannot be empty.")]
    public required string Password { get; set; }
}