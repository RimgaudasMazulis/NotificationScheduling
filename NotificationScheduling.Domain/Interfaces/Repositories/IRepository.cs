using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NotificationScheduling.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Entities { get; }
        DbContext DbContext { get; }
        /// <summary>
        /// Get all items of an entity by asynchronous method
        /// </summary>
        /// <returns></returns>
        Task<IList<T>> GetAllAsync();
        /// <summary>
        /// Get item of an entity based on predicate by asynchronous method
        /// </summary>
        /// <returns></returns>
        Task<IList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// Fin one item of an entity synchronous method
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        T Find(params object[] keyValues);
        /// <summary>
        /// Find one item of an entity by asynchronous method 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        Task<T> FindAsync(params object[] keyValues);
        /// <summary>
        /// Insert item into an entity by asynchronous method
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        Task InsertAsync(T entity, bool saveChanges = true);
        /// <summary>
        /// Insert multiple items into an entity by asynchronous method
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        Task InsertRangeAsync(IEnumerable<T> entities, bool saveChanges = true);
        /// <summary>
        /// Remove one item from an entity by asynchronous method
        /// </summary>
        /// <param name="id"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        Task DeleteAsync(int id, bool saveChanges = true);
        /// <summary>
        /// Remove one item from an entity by asynchronous method
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity, bool saveChanges = true);
        /// <summary>
        /// Remove multiple items from an entity by asynchronous method
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        Task DeleteRangeAsync(IEnumerable<T> entities, bool saveChanges = true);
    }
}
