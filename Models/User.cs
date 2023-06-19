using System.ComponentModel.DataAnnotations;

namespace RestaurMap.Models;

public class User
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
