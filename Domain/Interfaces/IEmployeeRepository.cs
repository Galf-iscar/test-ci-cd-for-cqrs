using CQRS_App.Domain.Models;

namespace CQRS_App.Domain.Interfaces;

public interface IEmployeeRepository 
{
    // Get all Employee tasks
    public Task<Employee> GetByIDAsync(long id);

    public Task AddAsync(Employee employee);
}
