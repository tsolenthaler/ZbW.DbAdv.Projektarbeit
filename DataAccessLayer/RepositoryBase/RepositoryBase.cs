using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace DataAccessLayer.RepositoryBase
{
    public abstract class RepositoryBase<M> : IRepositoryBase<M> where M : TEntity
    {
        protected SetupDB _context;

        protected RepositoryBase(SetupDB context)
        {
            _context = context;
        }

        public M GetSingle(int pkValue)
        {
            //using var context = _context;
            using var context = new SetupDB();
            var entities = context.Set<M>()
                .Where(e => e.Id == pkValue).ToArray();

            return entities[0];
        }

        public void Add(M entity)
        {
            //using var context = _context;
            using var context = new SetupDB();
            context.Set<M>().Add(entity);
            context.SaveChanges();
        }

        public virtual void Delete(M entity)
        {
            //using var context = _context;
            using var context = new SetupDB();
            var realEntity = context.Set<M>().Find(entity.Id);
            context.Set<M>().Remove(realEntity);
            context.SaveChanges();
        }

        public void Update(M entity)
        {
            //using var context = _context;
            using var context = new SetupDB();
            context.Entry(context.Set<M>().Find(entity.Id)).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }

        public M[] GetAll()
        {
            //using var context = _context;
            using var context = new SetupDB();
            return context.Set<M>().ToArray();
        }
    }
}
