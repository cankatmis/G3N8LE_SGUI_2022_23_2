using G3N8LE_ADT_2022_23_1.Data;
using System;
using System.Linq;

namespace G3N8LE_ADT_2022_23_1.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected SchoolDbContext context;
        protected Repository(SchoolDbContext ctx)
        {
            this.context = ctx;
        }
        public void Add(T entity)
        {
            context.Set<T>().Add(entity);

            context.SaveChanges();
        }
        public IQueryable<T> GetAll()
        {
            return this.context.Set<T>();
        }
        public abstract T GetOne(int id);
        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);

            context.SaveChanges();
        }
    }
}
