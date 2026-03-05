using UvsTask.Application.Abstractions;
using UvsTask.Domain.Entities;

namespace UvsTask.Application.Services;

public sealed class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    
    public EmployeeService(IEmployeeRepository repository) => _repository = repository;
    
    public async Task<Employee?> GetAsync(int employeeId, CancellationToken ct)
    {
        if (employeeId <= 0) throw new ArgumentOutOfRangeException(nameof(employeeId));
        return await _repository.GetAsync(employeeId, ct);
    }

    public async Task SetAsync(int employeeId, string employeeName, int employeeSalary, CancellationToken ct)
    {
        if (employeeId <= 0) throw new ArgumentOutOfRangeException(nameof(employeeId));
        if (string.IsNullOrEmpty(employeeName)) throw new ArgumentException(nameof(employeeName));
        if (employeeSalary <= 0) throw new ArgumentOutOfRangeException(nameof(employeeSalary));

        var employee = new Employee
        {
            Id = employeeId,
            Name = employeeName,
            Salary = employeeSalary,
        };

        await _repository.SaveAsync(employee, ct);
    }
}