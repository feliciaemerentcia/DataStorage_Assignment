using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Data.Repository;

namespace Data.Repositories;

public class StatusTypeRepository(DataContext context) : BaseRepository<StatusTypeEntity>(context), IStatusTypeRepository
{

}