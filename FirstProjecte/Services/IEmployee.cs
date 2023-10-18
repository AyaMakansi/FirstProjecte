using FirstProjecte.Model;

namespace FirstProjecte.Services;

public interface IEmployee
{
    Task<IEnumerable<Employee>> GetAll();
    
    Task<Employee> GetById(int id);
    
    Task<Employee> Add(Employee employee);
    
    Employee Update(Employee employee);
    
    Employee Delete(Employee employee);

    Task<IEnumerable<Employee>> Search(string name);


}