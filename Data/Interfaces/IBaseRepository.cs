using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null);
        Task<TEntity?> GetAsync(Expression <Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null);
        void Remove(TEntity entity);
        Task RollbackTransactionAsync();
        Task<int> SaveAsync();
        void Update(TEntity entity);
    }
}