using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Data.Repository;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    
}
