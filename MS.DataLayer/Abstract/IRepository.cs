using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.DataLayer.Abstract
{
    public interface IRepository
    {
        void AddNewEntity<T>(T entity);
        void AddRangeNewEntities<T>(IEnumerable<T> entity);
        void DeleteEntity<T>(T entity);
        void DeleteEntityRange<T>(IEnumerable<T> entity);
        void SaveChanges();

        void ExecuteInTransaction(Action action);

        T Get<T>(int key);
        IList<T> GetAll<T>();
    }
}
