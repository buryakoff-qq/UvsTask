using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UvsTask.Domain.Entities;

namespace UvsTask.Infrastructure;

public sealed class UvsDbContext : DbContext
{
    public UvsDbContext(DbContextOptions<UvsDbContext> options) : base(options) { }
    
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}