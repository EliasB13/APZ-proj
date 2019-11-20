using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Interfaces
{
	public interface IAsyncRepository<T>
	{
		Task<T> GetByIdAsync(int id);
		Task<List<T>> ListAllAsync();
		Task<List<T>> ListAllAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> includeExpression);
		Task<List<T>> ListAllAsync(Expression<Func<T, bool>> predicate);
		Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
		Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> includeExpression);
		Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
		Task<T> AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
	}
}
