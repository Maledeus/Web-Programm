using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WifiSD.Core.Entities;
using WifiSD.Core.Repositories.Base;
using WifiSD.Persistence.Repositories.DBContext;

namespace WifiSD.Persistence.Repositories.Base
{
    public abstract class BaseRepository : IBaseRepository
    {
        private MovieDbContext movieDbContext;

        #region ctor | dtor

        public BaseRepository()
        {
            this.movieDbContext = new MovieDbContext();
        }

        protected BaseRepository(MovieDbContext movieDbContext)
        {
            this.movieDbContext= movieDbContext;
        }

        #endregion

        public void Save()
        {
            this.movieDbContext.SaveChanges();
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await this.movieDbContext.SaveChangesAsync(cancellationToken);
        }

        
        #region [C]REATE

         public void Add<T>(T entity, bool saveImmediately)
            where T : class, IEntity
        {
            if (entity == null)
            {
                return;
            }

            this.movieDbContext.Add(entity);

            if (saveImmediately)
            {
                this.Save();
            }
        }

        public async Task AddAsync<T>(T entity, bool saveImmediately, CancellationToken cancellationToken)
            where T : class, IEntity
        {
            if (entity == null)
            {
                return;
            }

            await this.movieDbContext.AddAsync(entity);

            if (saveImmediately)
            {
                await this.SaveAsync(cancellationToken);
            }
        }

        #endregion


        #region [R]EAD

        public IQueryable<T> QueryFrom<T>(Expression<Func<T, bool>> whereFilter = null)
            where T : class, IEntity
        {
            var query = this.movieDbContext.Set<T>();
            if (whereFilter != null)
            {
                return query.Where(whereFilter);
            }

            return query;
        }

        #endregion


        #region [U]PDATE

        public T Update<T>(T entity, object key, bool saveImmediately)
            where T : class, IEntity
        {
            if (entity == null)
            {
                return null;
            }

            var toUpdate = this.movieDbContext.Set<T>().Find(key);

            if (toUpdate != null)
            {
                this.movieDbContext.Entry(toUpdate).CurrentValues.SetValues(entity);

                if (saveImmediately)
                {
                    this.Save();
                }

            }

            return toUpdate;

        }

        public async Task<T> UpdateAsync<T>(T entity, object key, bool saveImmediately, CancellationToken cancellationToken)
            where T : class, IEntity
        {
            if (entity == null)
            {
                return null;
            }

            var toUpdate = await this.movieDbContext.Set<T>().FindAsync(key);

            if (toUpdate != null)
            {
                this.movieDbContext.Entry(toUpdate).CurrentValues.SetValues(entity);

                if (saveImmediately)
                {
                    await this.SaveAsync(cancellationToken);
                }
            }

            return toUpdate;

        }

            #endregion


        #region [D]ELETE

            public void Remove<T>(T entity, bool saveImmediately)
            where T : class, IEntity
        {
            
                if (entity == null)
                {
                    return;
                }

                this.movieDbContext.Remove(entity);

                if (saveImmediately)
                {
                    this.Save();
                }
            
        }

        public async Task RemoveAsync<T>(T entity, bool saveImmediately, CancellationToken cancellationToken)
            where T : class, IEntity
        {
            if (entity == null)
            {
                return;
            }

            this.movieDbContext.Remove(entity);

            if (saveImmediately)
            {
                await this.SaveAsync(cancellationToken);
            }
        }

        public void RemoveByKey<T>(object key, bool saveImmediately)
            where T : class, IEntity
        {
            if (key == null)
            {
                return;
            }

            var toRemove = this.movieDbContext.Set<T>().Find(key);
            if (toRemove != null)
            {

                this.movieDbContext.Remove(toRemove);

                if (saveImmediately)
                {
                    this.Save();
                }
            }
            
        }

        public async Task RemoveByKeyAsync<T>(object key, bool saveImmediately, CancellationToken cancellationToken)
            where T : class, IEntity
        {
            if (key == null)
            {
                return;
            }

            var toRemove = await this.movieDbContext.Set<T>().FindAsync(key);
            if (toRemove != null)
            {

                this.movieDbContext.Remove(toRemove);

                if (saveImmediately)
                {
                    await this.SaveAsync(cancellationToken);
                }
            }
        }
        #endregion


    }
}
