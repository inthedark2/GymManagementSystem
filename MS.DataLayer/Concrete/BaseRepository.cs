using System;
using System.Collections.Generic;
using System.Linq;
using MS.DataLayer.Abstract;
using MS.DataLayer.Entities;

namespace MS.DataLayer.Concrete
{
    public class BaseRepository : IRepository
    {
        private readonly ManagmentSystemContext _dataContext;

        public T ProcessQuery<T>(Func<T> query)
        {
            return query();
        }

        public BaseRepository()
        {
            _dataContext = new ManagmentSystemContext();
        }

        public void AddNewEntity<T>(T entity)
        {
            Type type = typeof(T);
            _dataContext.Set(type).Add(entity);
        }

        public void AddRangeNewEntities<T>(IEnumerable<T> entity)
        {
            Type type = typeof(T);
            _dataContext.Set(type).AddRange(entity);
        }

        public void DeleteEntity<T>(T entity)
        {
            Type type = typeof(T);
            _dataContext.Set(type).Remove(entity);
        }

        public void DeleteEntityRange<T>(IEnumerable<T> entity)
        {
            Type type = typeof(T);
            _dataContext.Set(type).RemoveRange(entity);
        }

        public void ExecuteInTransaction(Action action)
        {
            using (var dbContextTransaction = _dataContext.Database.BeginTransaction())
            {
                try
                {
                    action();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
        }

        public void SaveChanges()
        {
            EfSaveChanges();
        }

        private void EfSaveChanges()
        {
            _dataContext.SaveChanges();
        }

        public T Get<T>(int key)
        {
            return ProcessQuery(() =>
            {
                var type = typeof(T);
                return (T)_dataContext.Set(type).Find(key);
            });
        }

        public IList<T> GetAll<T>()
        {
            return ProcessQuery(() =>
            {
                Type type = typeof(T);
                return ((IQueryable<T>)_dataContext.Set(type)).ToList();
            });
        }
    }
}
