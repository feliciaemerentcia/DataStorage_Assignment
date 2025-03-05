using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Data.Repository;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : BaseRepository<CustomerEntity>(context), ICustomerRepository
{

}
