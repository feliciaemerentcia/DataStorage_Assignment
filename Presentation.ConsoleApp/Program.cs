using Business.Services;
using Data.Context;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ConsoleApp;

var services = new ServiceCollection()
    .AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\DataStorage\\Data\\Databases\\local_database.mdf;Integrated Security=True;Connect Timeout=30"))
    .AddScoped<ProjectRepository>()
    .AddScoped<CustomerRepository>()
    .AddScoped<ProjectService>()
    .AddScoped<CustomerService>()
    .AddScoped<MenuDialogs>()
    .BuildServiceProvider();

var menuDialogs = services.GetRequiredService<MenuDialogs>();
await menuDialogs.Run();
