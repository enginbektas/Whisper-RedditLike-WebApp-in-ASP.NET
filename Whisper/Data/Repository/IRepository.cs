using Whisper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Whisper.Data.Repository
{
    public interface IRepository<T>
        where T : class, IEntity
    {
        Task<T> GetByAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetAllAsync();

        IQueryable<T> Where(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        Task AddAllAsync(List<T> entities);

        void Remove(T entity);

        void RemoveRange(List<T> values);

        T Update(T entity);

        #region Properties

        /// <summary>
        /// ilgili tabloya özel sorgu atmak istendiği zaman Repository'nin bu Table property üzerinden kullanılabilir.
        /// </summary>
        IQueryable<T> Table { get; }

        #endregion

    }
}
