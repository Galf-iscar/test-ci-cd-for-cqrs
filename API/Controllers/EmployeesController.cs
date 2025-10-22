using Horizon.Utils;
using Horizon.Utils.Commands.Abstractions.Mediator;
using Horizon.Utils.OperationCodes;
using Horizon.Utils.Output;
using Microsoft.AspNetCore.Mvc;
using CQRS_App.Application.Commands;
using CQRS_App.Application.DTOs.EmployeeDTO;
using CQRS_App.Application.Queries;

namespace CQRS_App.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[OperationPrefix("employees")]
public class EmployeesController(ICommandMediator mediator, IOutputFactory outputFactory)
    : ControllerBase 
{
    private readonly ICommandMediator _mediator = mediator;
    private readonly IOutputFactory _outputFactory = outputFactory;

    [HttpGet("{employeeId}")]
    [OperationCode("001")]
    public virtual async Task<IActionResult> GetEmployeeById(long employeeId, int pageNumber = 1, int pageSize = 50, CancellationToken ct = default)
    {
        EmployeeDTOResponse employeeDto = await _mediator.Send(new GetEmployeeByIdQuery(employeeId, pageNumber, pageSize, ct: ct));
        return _outputFactory.CreateOutput([employeeDto]);
    }

    [HttpPost("add")]
    [OperationCode("002")]
    public virtual async Task<IActionResult> CreateEmployee(EmployeeDTORequest employeeDto, int pageNumber = 1, int pageSize = 50, CancellationToken ct = default)
    {
        await _mediator.Send(new CreateEmployeeCommand(employeeDto));
        return _outputFactory.CreateOutput(Enumerable.Empty<object>(), returnCode: ServiceReturnCode.Created);
    }
}