using Entities.Models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MoreLinq;

namespace DAL.Repo
{//Semaine Repository Interface
    public interface ISemaineRepository : IGenericRepository<Semaine, int>
    {
        Semaine GetCurrent();
        Semaine GetLast();
    }

    //Semaine Repository
    public class SemaineRepository : ISemaineRepository, IDisposable
    {

        public readonly ApplicationDbContext context = new ApplicationDbContext();

        public IList<Semaine> GetAll()
        {

            IList<Semaine> query = (from c in context.Semaines select c).ToList<Semaine>();

            return query;
        }

        public Semaine GetSingle(int SemaineId)
        {

            var query = (from c in context.Semaines select c).FirstOrDefault<Semaine>(x => x.SemaineId == SemaineId);
            return query;
        }

        public Semaine GetCurrent()
        {

            var query = (from c in context.Semaines select c).FirstOrDefault<Semaine>(x => x.isCurrent == true);
            return query;
        }

        public Semaine GetLast()
        {

            var query = (from c in context.Semaines select c).ToList<Semaine>();
            Semaine latest = query.MaxBy(s => s.SemaineId);
            return latest;
        }

        public void Add(Semaine entity)
        {

            context.Semaines.Add(entity);
        }

        public void Delete(Semaine entity)
        {

            context.Semaines.Remove(entity);
        }

        public void Update(Semaine entity)
        {
            context.Entry(entity).State = EntityState.Modified;

        }

        public void Save()
        {
            context.SaveChanges();
        }

        void IDisposable.Dispose() { }
        public void Dispose() { context.Dispose(); }

    }
}
