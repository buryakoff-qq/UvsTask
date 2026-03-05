using UvsTask.Application.Abstractions;

namespace UvsTask.ConsoleApp;

internal sealed class CliApp
{
    private readonly IEmployeeService _employeeService;

    public CliApp(IEmployeeService employeeService) => _employeeService = employeeService;

    public async Task<int> RunAsync(string[] args, CancellationToken ct)
    {
        try
        {
            if (args.Length == 0)
            {
                PrintUsage();
                return 2;
            }

            var cmd = args[0];
            var parsed = Args.Parse(args);

            switch (cmd)
            {
                case "get-employee":
                {
                    var id = parsed.RequireInt("--employeeId");
                    var employee = await _employeeService.GetAsync(id, ct);

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
                    var id = parsed.RequireInt("--employeeId");
                    var name = parsed.RequireString("--employeeName");
                    var salary = parsed.RequireInt("--employeeSalary");

                    await _employeeService.SetAsync(id, name, salary, ct);

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

    private static void PrintUsage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("dotnet run set-employee --employeeId 5 --employeeName Steve --employeeSalary 123");
        Console.WriteLine("dotnet run get-employee --employeeId 5");
    }
}