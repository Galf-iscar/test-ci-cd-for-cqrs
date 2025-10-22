using Horizon.Utils.Exceptions;
using Microsoft.Extensions.Logging;
using test_ci_cd_for_cqrs.Domain.Interfaces;
using test_ci_cd_for_cqrs.Domain.Models;
using test_ci_cd_for_cqrs.Infrastructure.Data;

namespace test_ci_cd_for_cqrs.Infrastructure.Repositories
{
    public class EmployeeRepository(DBcontext context, ILogger<EmployeeRepository> logger)
        : IEmployeeRepository
    {
        private readonly DBcontext _context = context;
        private readonly ILogger<EmployeeRepository> _logger = logger;

        public async Task<Employee> GetByIDAsync(long id)
        {
            Employee? employee = await _context.Employees.FindAsync(id);
            if (employee is null)
            {
                throw new ItemNotFoundException($"employee with id:{id} not found");
            }

            return employee;
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }
    }
}
