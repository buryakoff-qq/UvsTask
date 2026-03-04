namespace UvsTask.Domain.Entities;

public sealed class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public int Salary { get; set; }
}