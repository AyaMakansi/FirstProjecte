using Microsoft.EntityFrameworkCore;

namespace FirstProjecte.Model;

public class ApplicationDbContext :DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
    {
        
    }
    public DbSet<Employee> Employees { get; set; }
    
}