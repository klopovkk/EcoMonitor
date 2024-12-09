using EcoMonitor.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcoMonitor.Core.Repository;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly EcoContext context;
    public Repository(EcoContext context)
    {
        this.context = context;
    }

    public IQueryable<T> Get()
    {
        IQueryable<T> list = context.Set<T>();
        return list;
    }
    public async Task Create(T entity, string createBody = null)
    {
        context.Set<T>().Add(entity);
        await context.SaveChangesAsync().ConfigureAwait(false);
    }
    public async Task<T> GetById(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }
    public async Task Update(T entity, string updateBody = null)
    {
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync().ConfigureAwait(false);
    }
    public async Task Delete(int id)
    {
        T entity = await context.Set<T>().FindAsync(id).ConfigureAwait(false);
        context.Remove(entity);
        await context.SaveChangesAsync();
    }
}