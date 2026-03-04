using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UvsTask.Application.Abstractions;
using UvsTask.Application.Services;
using UvsTask.Infrastructure;
using UvsTask.Infrastructure.Repositories;

namespace UvsTask.ConsoleApp;

internal sealed class Program
{
    static async Task<int> Main(string[] args)
    {
        try
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return 2;
            }

            Env.Load();
            
            var cs = Environment.GetEnvironmentVariable("DB_CONNECTION");
            if (string.IsNullOrWhiteSpace(cs))
                throw new InvalidOperationException("DB_CONNECTION env variable is not set.");
            
            var services = new ServiceCollection();

            services.AddDbContext<UvsDbContext>(o => o.UseNpgsql(cs));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            await using var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            
            var employeeService = scope.ServiceProvider.GetRequiredService<IEmployeeService>();
            var ct = CancellationToken.None;

            var cmd = args[0];

            switch (cmd)
            {
                case "get-employee":
                {
                    var id = RequireInt(args, "--employeeId");
                    var employee = await employeeService.GetAsync(id, ct);

                    if (employee is null)
                    {
                        Console.WriteLine($"Employee {id} not found");
                        return 1;
                    }

                    Console.WriteLine($"EmployeeId={employee.Id}, Name={employee.Name}, Salary={employee.Salary}");
                    return 0;
                }
                case "set-employee":
                {
                    var id = RequireInt(args, "--employeeId");
                    var name = RequireString(args, "--employeeName");
                    var salary = RequireInt(args, "--employeeSalary");

                    await employeeService.SetAsync(id, name, salary, ct);

                    Console.WriteLine("OK");
                    return 0;
                }
                default:
                    PrintUsage();
                    return 2;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return 2;
        }
    }

    private static int RequireInt(string[] args, string key)
    {
        var value = RequireString(args, key);
        return !int.TryParse(value, out var parsedValue) ? throw new ArgumentException($"{key} must be INT") : parsedValue;
    }

    private static string RequireString(string[] args, string key)
    {
        var index = Array.IndexOf(args, key);
        if (index < 0 || index == args.Length - 1)
            throw new ArgumentException($"{key} is required");
        return args[index + 1];
    }

    private static void PrintUsage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("dotnet run set-employee --employeeId 5 --employeeName Steve --employeeSalary 123");
        Console.WriteLine("dotnet run get-employee --employeeId 5");
    }
}