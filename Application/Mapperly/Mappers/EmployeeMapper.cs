using Horizon.Utils.Mapper;
using Riok.Mapperly.Abstractions;
using CQRS_App.Application.DTOs.EmployeeDTO;
using CQRS_App.Domain.Models;

namespace CQRS_App.Application.Mapperly.Mappers;

/// <summary>  
/// Provides mapping methods for converting objects.  
/// </summary>  
[Mapper]
[MapperFor(typeof(Employee), typeof(EmployeeDTORequest), nameof(MapToEmployeeDtoRequest))]
[MapperFor(typeof(Employee), typeof(EmployeeDTOResponse), nameof(MapToEmployeeDtoResponse))]
[MapperFor(typeof(Employee), typeof(DefaultEmployeeDto), nameof(MapToDefaultEmployeeDto))]
[MapperFor(typeof(EmployeeDTORequest), typeof(Employee), nameof(MapToEmployee))]
[MapperFor(typeof(EmployeeDTORequest), typeof(Employee), nameof(UpdateEmployeeTask))]

public static partial class EmployeeMapper
{
    // mapping from empelyee to dto request
    public static partial EmployeeDTORequest MapToEmployeeDtoRequest(Employee employee);

    public static partial List<EmployeeDTORequest> MapToEmployeeDtoRequest(List<Employee> employees);

    public static partial IEnumerable<EmployeeDTORequest> MapToEmployeeDtoRequest(IEnumerable<Employee> employees);

    // mapping from employee to dto response
    public static partial EmployeeDTOResponse MapToEmployeeDtoResponse(Employee employee);

    public static partial List<EmployeeDTOResponse> MapToEmployeeDtoResponse(List<Employee> employees);

    public static partial IEnumerable<EmployeeDTOResponse> MapToEmployeeDtoResponse(IEnumerable<Employee> employees);

    // mapping from employee to default dto
    public static partial DefaultEmployeeDto MapToDefaultEmployeeDto(Employee employee);

    public static partial List<DefaultEmployeeDto> MapToDefaultEmployeeDto(List<Employee> employees);

    public static partial IEnumerable<DefaultEmployeeDto> MapToDefaultEmployeeDto(IEnumerable<Employee> employees);

    // mapping from default employee to entity
    public static partial Employee MapToEmployee(EmployeeDTORequest employeeDto);

    public static partial List<Employee> MapToEmployee(List<EmployeeDTORequest> employeesDto);

    public static partial IEnumerable<Employee> MapToEmployee(IEnumerable<EmployeeDTORequest> employeesDto);

    // map to existing employee 
    [MapperIgnoreTarget(nameof(Employee.EmployeeID))]
    public static partial void UpdateEmployeeTask(EmployeeDTORequest employeeDto, Employee employee);
}
