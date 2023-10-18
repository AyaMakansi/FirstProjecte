using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FirstProjecte.Model;

public class Employee
{    [Key]
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
    
    public string? Image{ get; set; }
}