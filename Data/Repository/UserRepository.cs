using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Data.Repository;

namespace Data.Repositories;

public class UserRepository(DataContext context) : BaseRepository<UserEntity>(context), IUserRepository
{

}
