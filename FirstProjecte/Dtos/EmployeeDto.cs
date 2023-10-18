using System.Diagnostics.CodeAnalysis;

namespace FirstProjecte.Dtos;

public class EmployeeDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    
    public string Address { get; set; }
    [AllowNull]
    public IFormFile Image { get; set; }
}