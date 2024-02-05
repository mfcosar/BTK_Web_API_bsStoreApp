using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
        where T :class
    { // abstract class olunca new'lenemez

        protected readonly RepositoryContext _context; //injection. neden protected? Bu abstract class'ı kalıtan alt sınıfların context'e ulaşabilmeleri için. private olsaydı ulaşılamazdı
        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
            
        }
        public void Create(T entity) => _context.Set<T>().Add(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
            _context.Set<T>().AsNoTracking() :
            _context.Set<T>();


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ?
            _context.Set<T>().Where(expression).AsNoTracking() :
            _context.Set<T>().Where(expression);
        public void Update(T entity) => _context.Set<T>().Update(entity);
    }
}
