using Microsoft.EntityFrameworkCore;

namespace FirstProjecte.Services;

public class EmployeeService : IEmployee
{
    private readonly ApplicationDbContext _context;

    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Employee>> GetAll()
    { 
      return await _context.Employees.
                  OrderBy(r=>r.FullName).ToListAsync();
    }

    public async Task<Employee> GetById(int id)
    {
       
        return await _context.Employees.SingleOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Employee> Add(Employee employee)
    {
      await _context.Employees.AddAsync(employee);
                _context.SaveChanges();
                return employee;
    }

    public Employee Update(Employee employee)
    {
        _context.Employees.Update(employee);
                _context.SaveChanges();
                return employee;
    }

    public Employee Delete(Employee employee)
    {
         _context.Employees.Remove(employee);
                _context.SaveChanges();
                return employee;
    }

    public async Task<IEnumerable<Employee>> Search(string name)
    {
        IQueryable<Employee> query = _context.Employees;
            
        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(e => e.FullName.Contains(name));

        }
          return await query.ToListAsync();
    }
    
}