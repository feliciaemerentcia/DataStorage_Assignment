using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task CreateProjectAsync(ProjectRegistrationForm form);
        Task<bool> DeleteProjectsAsync(Expression<Func<ProjectEntity, bool>> expression);
        Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync();
        Task<ProjectEntity?> GetProjectByNameAsync(string title);
        Task<Project> UpdateProjectsAsync(ProjectUpdateForm form);
    }
}