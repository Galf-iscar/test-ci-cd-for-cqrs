using Horizon.Utils.Commands.Abstractions;
using Horizon.Utils.Mapper;
using test_ci_cd_for_cqrs.Application.DTOs.EmployeeDTO;
using test_ci_cd_for_cqrs.Domain.Interfaces;
using test_ci_cd_for_cqrs.Domain.Models;

namespace test_ci_cd_for_cqrs.Application.Commands
{
    // Command to create a new employee
    public sealed record CreateEmployeeCommand(EmployeeDTORequest employeeDto, int pageNumer = 1, int pageSize = 50, string? sortBy = null)
        : ICommand;

    // Handler for processing CreateEmployeeCommand
    public sealed class CreateEmployeeCommandHandler(
        IEmployeeRepository employeeRepository,
        IMappingService mapper)
        : ICommandHandler<CreateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IMappingService _mapper = mapper;

        public async Task<Unit> Handle(CreateEmployeeCommand command, CommandContext ctx, CancellationToken ct)
        {
            Employee? employee = _mapper.Map<EmployeeDTORequest, Employee>(command.employeeDto);
            await _employeeRepository.AddAsync(employee);
            return Unit.Value;
        }
    }
}