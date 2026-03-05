using UvsTask.Domain.Entities;

namespace UvsTask.Application.Abstractions;

public interface IEmployeeRepository
{
    Task<Employee?> GetAsync(int employeeId, CancellationToken ct);
    
    Task SaveAsync(Employee employee, CancellationToken ct);
}