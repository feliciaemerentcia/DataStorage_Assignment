using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Data.Context;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CustomerEntity> Customers { get; set; }

    public DbSet<UserEntity> Users { get; set; }

    public DbSet<ProductEntity> Products { get; set; }

    public DbSet<StatusTypeEntity> StatusTypes { get; set; }

    public DbSet<ProjectEntity> Projects { get; set; }

}
