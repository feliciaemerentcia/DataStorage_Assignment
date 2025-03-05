using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Business.Services;

public class ProjectService(ICustomerService customerService, IProjectRepository projectRepository) : IProjectService
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task CreateProjectAsync(ProjectRegistrationForm form)
    {

        await _projectRepository.BeginTransactionAsync();

        try
        {

            var customer = await _customerService.GetCustomerAsync(form.Customer.CustomerName);
            if (customer == null)
            {
                var result = await _customerService.CreateCustomerAsync(form.Customer);
                if (result)
                    customer = await _customerService.GetCustomerAsync(form.Customer.CustomerName);

                else
                    throw new Exception();
            }

            var projectEntity = await _projectRepository.GetAsync(x => x.Title == form.Title);

            await _projectRepository.AddAsync(ProjectFactory.Create(form));
            await _projectRepository.SaveAsync(); 

            await _projectRepository.CommitTransactionAsync();
        }
        catch
        {
            await _projectRepository.RollbackTransactionAsync();
        }
    }


    public async Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync()
    {
        return await _projectRepository.GetAllAsync(query =>
        query.Include(project => project.Customer)
        );
    }

    public async Task<ProjectEntity?> GetProjectByNameAsync(string title)
    {
        var result = await _projectRepository.GetAsync(x => x.Title == title, query =>
        query.Include(project => project.Customer)
        .Include(project => project.Product)
        .Include(project => project.StatusType)
        .Include(project => project.User)
        );
        return result;
    }

    public async Task<Project> UpdateProjectsAsync(ProjectUpdateForm form)
    {
        await _projectRepository.BeginTransactionAsync();

        try
        {
            var projectEntity = ProjectFactory.Create(form);
            _projectRepository.Update(projectEntity);
            await _projectRepository.CommitTransactionAsync();
            return new Project
            {
                Title = projectEntity.Title,
                Description = projectEntity.Description,
                StartDate = projectEntity.StartDate,
                EndDate = projectEntity.EndDate,
            };
        }
        catch
        {
            await _projectRepository.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<bool> DeleteProjectsAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        await _projectRepository.BeginTransactionAsync();

        try
        {
            var projectEntity = await _projectRepository.GetAsync(expression); 
            if (projectEntity == null)
            {
                return false; 
            }
            _projectRepository.Remove(projectEntity);
            await _projectRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _projectRepository.RollbackTransactionAsync();
            return false;
        }
    }

}
