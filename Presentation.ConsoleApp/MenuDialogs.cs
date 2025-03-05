using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Presentation.ConsoleApp;

public class MenuDialogs(IProjectService projectService, ICustomerService customerService)
{

    private readonly IProjectService _projectService = projectService;
    private readonly ICustomerService _customerService = customerService;

    public async Task Run()
    {
        while (true)
        {
            MenuOptions();
        }
    }


    private void MenuOptions()
    {
        Console.Clear();
        Console.WriteLine("HUVUDMENY");
        Console.WriteLine($"{"1. ",-5} Skapa nytt projekt");
        Console.WriteLine($"{"2. ",-5} Se befintliga projekt");
        Console.WriteLine($"{"3. ",-5} Ändra/uppdatera ett projekt");
        Console.WriteLine($"{"4. ",-5} Radera project");
        Console.Write("Vänligen välj något av ovanstående menyval: ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                CreateOption();
                break;

            case "2":
                ViewOption();
                break;

            case "3":
                UpdateOption();
                break;

            case "4":
                DeleteOption();
                break;

            default:
                InvalidOption();
                break;
        }

        Console.ReadKey();
    }

    private async Task CreateOption()
    {
        ProjectRegistrationForm projectRegistrationForm = ProjectFactory.Create();

        Console.Clear();

        Console.WriteLine("Vänligen ange en titel: ");
        projectRegistrationForm.Title = Console.ReadLine()!;

        Console.WriteLine("Ange en projektbeskrivning: ");
        projectRegistrationForm.Description = Console.ReadLine()!;

        Console.WriteLine("Ange namnet på kunden: ");
        string customerName = Console.ReadLine()!;

        var customer = await _customerService.GetCustomerAsync(customerName);

        if (customer == null)
        {
            var customerForm = new CustomerRegistrationForm { CustomerName = customerName };
            await _customerService.CreateCustomerAsync(customerForm); 

            customer = await _customerService.GetCustomerAsync(customerName);
        }

        Console.WriteLine($"Kunden vald: {customer.CustomerName}");

        Console.WriteLine("Ange startdatum (åååå-mm-dd): ");
        projectRegistrationForm.StartDate = DateTime.Parse(Console.ReadLine()!);

        Console.WriteLine("Ange slutdatum (åååå-mm-dd): ");
        projectRegistrationForm.EndDate = DateTime.Parse(Console.ReadLine()!);

        Console.WriteLine("Skapa projektet (ja(nej): ");
        string answer = Console.ReadLine()!.ToLower();

        if (answer == "ja")
        {
            try
            {
                await _projectService.CreateProjectAsync(projectRegistrationForm);
                OutputDialog("Projektet skapades framgångsrikt.");
            }
            catch
            {
                OutputDialog($"Ett fel uppstod vid skapandet av projektet");
            }

        }
        else
        {
            OutputDialog("Projektet skapades inte.");
        }

    }

    private async Task ViewOption()
    {
        Console.Clear();
        Console.WriteLine("Alla project");

        var projects = await _projectService.GetAllProjectsAsync();
        foreach (var project in projects)
        {
            Console.WriteLine($"{"Id: ",-5}{project.Id}");
            Console.WriteLine($"{"Titel: ",-5}{project.Title}");
            Console.WriteLine($"{"Beskrivning: ",-5}{project.Description}");
            Console.WriteLine($"{"Startdatum: ",-5}{project.StartDate:yyyy-MM-dd}");
            Console.WriteLine($"{"Slutdatum: ",-5}{project.EndDate:yyyy-MM-dd}");
            Console.WriteLine($"{"Kund: ",-5}{project.Customer.CustomerName}");
            Console.WriteLine(" ");
        }
    }

    private void UpdateOption()
    {

    }

    private void DeleteOption()
    {
        Console.Clear();
        Console.WriteLine("Vänligen ange projekttiteln för att radera projektet: ");

        string title = Console.ReadLine()!;

        Expression<Func<ProjectEntity, bool>> expression = project => project.Title == title;

        bool result = _projectService.DeleteProjectsAsync(expression);

        if (result)
        {
            OutputDialog($"Projektet '{title}' har raderats.");
        }
        else
        {
            OutputDialog($"Projektet med titeln '{title}' finns inte, eller raderingen misslyckades.");
        }

    }

    private void InvalidOption()
    {
        Console.Clear();
        Console.WriteLine("Please enter a valid option.");
    }

    private void OutputDialog(string message)
    {
        Console.Clear();
        Console.WriteLine(message);
    }
}
