using UvsTask.Domain.Entities;

namespace UvsTask.Application.Abstractions;

public interface IEmployeeService
{
    Task<Employee?> GetAsync(int employeeId, CancellationToken ct);
    
    Task SetAsync(int employeeId, string employeeName, int employeeSalary, CancellationToken ct);
}