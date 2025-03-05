using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        if (await _customerRepository.ExistsAsync(x => x.CustomerName == form.CustomerName))
            return false;

        await _customerRepository.BeginTransactionAsync();

        try
        {
            var customerEntity = await _customerRepository.GetAsync(x => x.CustomerName == form.CustomerName);

            await _customerRepository.AddAsync(CustomerFactory.Create(form));
            await _customerRepository.SaveAsync();

            await _customerRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _customerRepository.RollbackTransactionAsync();
            return false;
        }
    }

    public async Task<Customer?> GetCustomerAsync(string customerName)
    {
        var entity = await _customerRepository.GetAsync(x => x.CustomerName == customerName);

        return CustomerFactory.Create(entity!);
    }

}
