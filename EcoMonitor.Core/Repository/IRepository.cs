using EcoMonitor.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcoMonitor.Core.Repository;

public interface IRepository<T>
    where T : BaseEntity
{
    public IQueryable<T> Get();
    Task<T> GetById(int id);
    Task Create(T entity, string createBody = null);
    Task Update(T entity, string modifieBody = null);
    Task Delete(int id);
}