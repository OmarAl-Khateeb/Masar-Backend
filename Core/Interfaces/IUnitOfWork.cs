using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces
{
    public interface IUnitOfWork<TUContext> : IDisposable where TUContext : DbContext
    {
        IGenericRepository<TEntity, TContext> Repository<TEntity, TContext>() where TEntity : BaseEntity where TContext : DbContext;
        Task<int> Complete();
    }
}