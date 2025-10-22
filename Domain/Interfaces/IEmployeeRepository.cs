using test_ci_cd_for_cqrs.Domain.Models;

namespace test_ci_cd_for_cqrs.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        // Get all Employee tasks
        public Task<Employee> GetByIDAsync(long id);

        public Task AddAsync(Employee employee);
    }
}
