using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Web.Aplication.Interface.Repositories.Common;
using Web.Domain.Common.Interfaces;
using Web.Infrastructure.DbContext;

namespace Web.Infrastructure.Repositories.Common
{
    public abstract class BaseGenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class, IEntity<TKey>
    {
        protected ApplicationDbContext _context;
        private DbSet<T> _dbSet;

        public BaseGenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        /// <summary>
        /// Method add entity to db
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        public async Task Add(T obj, bool commit = true)
        {
            // add object to entity
            _dbSet.Add(obj);

            // if commit save to db
            if (commit)
                await Commit();
        }

        /// <summary>
        /// Method using save changes to database
        /// </summary>
        /// <returns></returns>
        public async Task Commit()
        {
            // save changes to db
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method using delete object 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="commit"></param>
        /// <returns>List<Object></returns>
        public async Task Delete(T obj, bool commit = true)
        {
            // remove object 
            _dbSet.Remove(obj);

            // if commit save to db
            if (commit)
                await Commit();
        }


        /// <summary>
        /// Method using find, orderby object by conditions
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns>IEnumerable<T></returns>
        public T FirstOrDefault(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
        {
            // get object from database
            IQueryable<T> query = _dbSet;

            // if fillter
            if (filter != null)
            {
                // fillter by condition
                query = query.Where(filter);
            }

            // get list properties to display
            var properties = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // loop properties
            foreach (var includeProperty in properties)
            {
                // Get the desired properties of the object
                query = query.Include(includeProperty);
            }

            // if order by
            if (orderBy != null)
            {
                // sort query
                query = orderBy(query);
            }

            // return result
            return query.FirstOrDefault();
        }

        public bool Any(Expression<Func<T, bool>>? filter = null)
        {
            // get object from database only query data
            IQueryable<T> query = _dbSet.AsNoTracking();


            // if fillter
            if (filter != null)
            {
                // fillter by condition
                query = query.Where(filter);
            }

            return query.Any();

        }

        /// <summary>
        /// method using get data from table 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns>IQueryable<Object></returns>
        public async Task<ICollection<T>> QueryAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", int pageSize = 0, int page = -1)
        {
            // get object from database only query data
            IQueryable<T> query = _dbSet.AsNoTracking();


            // if fillter
            if (filter != null)
            {
                // fillter by condition
                query = query.Where(filter);
            }

            // get list properties to display
            var properties = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // loop properties
            foreach (var includeProperty in properties)
            {
                // Get the desired properties of the object
                query = query.Include(includeProperty);
            }


            // if order by
            if (orderBy != null)
            {
                // sort query
                query = orderBy(query);
            }

            //if paging
            if (pageSize > 0 && page > -1)
            {
                query = query.Skip(pageSize * page).Take(pageSize);
            }

            // return result
            return await query.ToListAsync();
        }

        public async Task<ICollection<TResult>> QueryAndSelectAsync<TResult>(Expression<Func<T, int, TResult>> selector, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                string includeProperties = "", int pageSize = 0, int page = -1) where TResult : class
        {
            // get object from database only query data
            IQueryable<T> query = _dbSet.AsNoTracking();


            // if fillter
            if (filter != null)
            {
                // fillter by condition
                query = query.Where(filter);
            }

            // get list properties to display
            var properties = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // loop properties
            foreach (var includeProperty in properties)
            {
                // Get the desired properties of the object
                query = query.Include(includeProperty);
            }


            // if order by
            if (orderBy != null)
            {
                // sort query
                query = orderBy(query);
            }

            //if paging
            if (pageSize > 0 && page > -1)
            {
                query = query.Skip(pageSize * page).Take(pageSize);
            }

            //project and return

            // return result
            return await query.Select(selector).ToListAsync();

        }

        /// <summary>
        /// Get Object by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns>Object</returns>
        public async Task<T?> Get(TKey id, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties.Split(
                new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync(x => x.Id!.Equals(id));
        }

        public async Task<T?> Get(Expression<Func<T, bool>> filter, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync(filter);
        }
        public async Task<List<T>> Get(string? includeProperties = "")
        {
            if (string.IsNullOrEmpty(includeProperties))
                return await _dbSet.ToListAsync();
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        public async Task Update(T obj, bool commit = true)
        {
            _dbSet.Update(obj);
            if (commit)
                await _context.SaveChangesAsync();
        }

        public async Task TruncateTable(string tableName)
        {
            string cmd = $"TRUNCATE TABLE {tableName}";
            await _context.Database.ExecuteSqlRawAsync(cmd);
        }
    }


}
