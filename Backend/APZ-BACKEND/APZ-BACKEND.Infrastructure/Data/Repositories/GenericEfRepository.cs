using APZ_BACKEND.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Infrastructure.Data.Repositories
{
	public class EfRepository<T> : IAsyncRepository<T> where T : class
	{
		protected readonly ApplicationContext dbContext;
		protected DbSet<T> dbSet;

		public EfRepository(ApplicationContext dbContext)
		{
			this.dbContext = dbContext;
			dbSet = dbContext.Set<T>();
		}

		public virtual async Task<T> GetByIdAsync(int id)
		{
			return await dbSet.FindAsync(id);
		}

		public async Task<List<T>> ListAllAsync()
		{
			return await dbSet.AsNoTracking().ToListAsync();
		}

		public async Task<T> AddAsync(T entity)
		{
			await dbSet.AddAsync(entity);
			await dbContext.SaveChangesAsync();

			return entity;
		}

		public async Task UpdateAsync(T entity)
		{
			dbContext.Entry(entity).State = EntityState.Modified;
			await dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(T entity)
		{
			dbSet.Remove(entity);
			await dbContext.SaveChangesAsync();
		}

		public async Task<List<T>> ListAllAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> includeExpression)
		{
			return await dbSet.Where(predicate).Include(includeExpression).ToListAsync();
		}

		public async Task<List<T>> ListAllAsync(Expression<Func<T, bool>> predicate)
		{
			return await dbSet.Where(predicate).ToListAsync();
		}

		public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
		{
			return await dbSet.SingleOrDefaultAsync(predicate);
		}

		public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
		{
			return await dbSet.AnyAsync(predicate);
		}
	}
}
