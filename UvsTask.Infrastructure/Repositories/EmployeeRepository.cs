using Microsoft.EntityFrameworkCore;
using UvsTask.Application.Abstractions;
using UvsTask.Domain.Entities;

namespace UvsTask.Infrastructure.Repositories;

public sealed class EmployeeRepository : IEmployeeRepository
{
    private readonly UvsDbContext _context;
    
    public EmployeeRepository(UvsDbContext context) => _context = context;
    
    public async Task<Employee?> GetAsync(int employeeId, CancellationToken ct)
    {
        return await _context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == employeeId, ct);
    }

    public async Task SaveAsync(Employee employee, CancellationToken ct)
    {
        var existingEmployee = await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == employee.Id, ct);

        if (existingEmployee is null)
        {
            await _context.Employees.AddAsync(employee, ct);
        }
        else
        {
            existingEmployee.Name = employee.Name;
            existingEmployee.Salary = employee.Salary;
        }
        await _context.SaveChangesAsync(ct);
    }
}