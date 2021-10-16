using JobHunt.Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Repository
{
    public class JobHuntRepository<T> : IRepository<T> where T : class
    {
        protected JobHuntEntities _db { get; set; }

        public IQueryable<T> All {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected DbSet<T> _table = null;
        public JobHuntRepository()
        {
            _db = new JobHuntEntities();
            _table = _db.Set<T>();
        }
        public JobHuntRepository(JobHuntEntities db)
        {
            _db = db;
            _table = _db.Set<T>();
        }

        public IEnumerable<T> SelectAll()
        {
            return _table.ToList();
        }

        public T SelectById(object id)
        {
            try
            {
                return _table.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public T Insert(T item)
        {
            return _table.Add(item);
        }

        public void Update(T item)
        {
            _table.Attach(item);
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = _table.Find(id);
            _table.Remove(existing);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
