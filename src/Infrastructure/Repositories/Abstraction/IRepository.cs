using System.Linq.Expressions;

namespace Infrastructure.Repositories.Abstraction;

public interface IRepository<T>
{
    void Add(T entity);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void AddRange(IEnumerable<T> entities);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Update(IEnumerable<T> entities);
    T FindOne(Expression<Func<T, bool>> predicate);
    Task<T> FindOneAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    T FindSingle(Expression<Func<T, bool>> predicate);
    Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    T Get(object id);
    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
    Task<T> GetAsync(object id, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<int> ExecuteSqlRawAsync(string sql, object[] parameters, CancellationToken cancellationToken = default);
    int ExecuteSqlRaw(string sql, object[] parameters);
    Task<IEnumerable<T>> FromSqlRawAsync(string sql, object[] parameters, CancellationToken cancellationToken = default);
    IEnumerable<T> FromSqlRaw(string sql, params object[] parameters);
}