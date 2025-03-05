using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Data.Repository;

namespace Data.Repositories;

public class ProductRepository(DataContext context) : BaseRepository<ProductEntity>(context), IProductRepository
{
    
}
