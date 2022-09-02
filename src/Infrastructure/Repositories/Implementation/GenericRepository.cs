using System.Linq.Expressions;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories.Abstraction;

namespace Infrastructure.Repositories.Implementation;

public class GenericRepository<T> : IRepository<T> where T : class
{
    protected Context _context;

    public GenericRepository(Context context)
    {
        _context = context;
    }

    public virtual void Add(T entity)
    {
        _context.Add(entity);
    }
    public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(entity, cancellationToken);
    }
    public virtual void AddRange(IEnumerable<T> entities)
    {
        _context.AddRange(entities);
    }
    public virtual async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await _context.AddRangeAsync(entities, cancellationToken);
    }
    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
        var result = _context.Set<T>()
            .AsQueryable()
            .Where(predicate)
            .ToList();

        return result;
    }
    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var result = await _context.Set<T>()
                .AsQueryable()
                .Where(predicate)
                .ToListAsync(cancellationToken);

        return result;
    }
    public virtual T FindOne(Expression<Func<T, bool>> predicate)
    {
        var result = _context.Set<T>()
                .AsQueryable()
                .FirstOrDefault(predicate);

        return result;
    }
    public virtual Task<T> FindOneAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var result = _context.Set<T>()
                .AsQueryable()
                .FirstOrDefaultAsync(predicate, cancellationToken);

        return result;
    }
    public virtual T FindSingle(Expression<Func<T, bool>> predicate)
    {
        var result = _context.Set<T>()
                .AsQueryable()
                .SingleOrDefault(predicate);

        return result;
    }
    public virtual Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var result = _context.Set<T>()
                .AsQueryable()
                .SingleOrDefaultAsync(predicate, cancellationToken);

        return result;
    }
    public virtual T Get(object id)
    {
        var result = _context.Find<T>(id);

        return result;
    }
    public virtual IEnumerable<T> GetAll()
    {
        var result = _context.Set<T>()
                .ToList();

        return result;
    }
    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _context.Set<T>()
            .ToListAsync(cancellationToken);

        return result;
    }
    public virtual async Task<T> GetAsync(object id, CancellationToken cancellationToken = default)
    {
        var result = await _context.FindAsync<T>(id);

        return result;
    }
    public virtual int SaveChanges()
    {
        var result = _context.SaveChanges();

        return result;
    }
    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await _context.SaveChangesAsync(cancellationToken);

        return result;
    }
    public virtual void Update(T entity)
    {
        _context.Update(entity);
    }
    public virtual void Update(IEnumerable<T> entities)
    {
        _context.UpdateRange(entities);
    }
    public virtual int ExecuteSqlRaw(string sql, params object[] parameters)
    {
        var result = _context.Database.ExecuteSqlRaw(sql, parameters);

        return result;
    }
    public virtual async Task<int> ExecuteSqlRawAsync(string sql, object[] parameters, CancellationToken cancellationToken = default)
    {
        var result = await _context.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);

        return result;
    }
    public virtual IEnumerable<T> FromSqlRaw(string sql, params object[] parameters)
    {
        var result = _context.Set<T>()
                .FromSqlRaw(sql, parameters)
                .ToList();

        return result;
    }
    public virtual async Task<IEnumerable<T>> FromSqlRawAsync(string sql, object[] parameters, CancellationToken cancellationToken = default)
    {
        var result = await _context.Set<T>()
                .FromSqlRaw(sql, parameters)
                .ToListAsync(cancellationToken);

        return result;
    }
}