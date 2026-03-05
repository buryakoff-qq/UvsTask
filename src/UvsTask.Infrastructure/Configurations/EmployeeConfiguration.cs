using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UvsTask.Domain.Entities;

namespace UvsTask.Infrastructure.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");
        
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("employeeid");

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(128)
            .HasColumnName("employeename");

        builder.Property(e => e.Salary)
            .HasColumnName("employeesalary")
            .IsRequired();
    }
}