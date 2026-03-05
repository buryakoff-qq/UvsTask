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
            Env.Load();

            var cs = Environment.GetEnvironmentVariable("DB_CONNECTION");
            if (string.IsNullOrWhiteSpace(cs))
                throw new InvalidOperationException("DB_CONNECTION env variable is not set.");

            var services = new ServiceCollection();

            services.AddDbContext<UvsDbContext>(o => o.UseNpgsql(cs));
            
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddSingleton<CliApp>();

            await using var serviceProvider = services.BuildServiceProvider();

            var app = serviceProvider.GetRequiredService<CliApp>();
            
            return await app.RunAsync(args, CancellationToken.None);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return 2;
        }
    }
}