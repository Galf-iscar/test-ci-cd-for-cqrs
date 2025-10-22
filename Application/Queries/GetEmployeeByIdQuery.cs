using Horizon.Utils.Commands.Abstractions;
using Horizon.Utils.Exceptions;
using Horizon.Utils.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using test_ci_cd_for_cqrs.Application.DTOs.EmployeeDTO;
using test_ci_cd_for_cqrs.Domain.Interfaces;
using test_ci_cd_for_cqrs.Domain.Models;

namespace test_ci_cd_for_cqrs.Application.Queries
{
    // Query to get an employee by their ID with optional pagination and sorting parameters
    public sealed record GetEmployeeByIdQuery(long employeeId, int pageNumer = 1, int pageSize = 50, string? sortBy = null, CancellationToken ct = default)
        : IQuery<EmployeeDTOResponse>;

    // Handler for processing GetEmployeeByIdQuery
    public sealed class GetEmployeeByIdQueryHandler(
        ILogger<GetEmployeeByIdQueryHandler> logger,
        IReadDbContext dbContext,
        IMappingService mapper)
        : IQueryHandler<GetEmployeeByIdQuery, EmployeeDTOResponse>
    {
        private readonly IReadDbContext _context = dbContext;
        private readonly ILogger<GetEmployeeByIdQueryHandler> _logger = logger;
        private readonly IMappingService _mapper = mapper;

        public async Task<EmployeeDTOResponse> Handle(GetEmployeeByIdQuery command, CommandContext ctx, CancellationToken ct)
        {
            Employee? employee = await _context.Query<Employee>().FirstOrDefaultAsync(e => e.EmployeeID == command.employeeId, ct);
            if (employee is null)
            {
                string errorMessage = $"No Employee with id = {command.employeeId}.";
                _logger.LogError(errorMessage);
                throw new ItemNotFoundException(errorMessage);
            }

            EmployeeDTOResponse employeeDTO = _mapper.Map<Employee, EmployeeDTOResponse>(employee);
            return employeeDTO;
        }
    }
}
