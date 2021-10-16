using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Repository
{
    interface IRepository<T> where T : class
    {
        IQueryable<T> All { get; }
        IEnumerable<T> SelectAll();
        T SelectById(object id);
        //void Insert(T item);
        T Insert(T item);
        void Update(T item);
        void Delete(object id);
        void SaveChanges();
    }
}
