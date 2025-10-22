using Horizon.Utils.Exceptions;
using CQRS_App.Domain.Interfaces;
using CQRS_App.Domain.Models;
using CQRS_App.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace CQRS_App.Infrastructure.Repositories;

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
