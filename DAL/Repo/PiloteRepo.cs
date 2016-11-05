using DAL.Models;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    //Identity Repository Interface
    public interface IPiloteRepository : IGenericRepository<Pilote, int>
    {
    }

    //User Repository
    public class PiloteRepository : IPiloteRepository, IDisposable
    {

        public readonly ApplicationDbContext context = new ApplicationDbContext();

        public IList<Pilote> GetAll()
        {

            IList<Pilote> query = (from c in context.Pilotes select c).ToList<Pilote>();

            return query;
        }


        public Pilote GetSingle(int PiloteId)
        {

            var query = (from c in context.Pilotes select c).FirstOrDefault<Pilote>(x => x.PiloteId == PiloteId);
            return query;
        }



        public void Add(Pilote entity)
        {
            context.Pilotes.Add(entity);
        }

        public void Delete(Pilote entity)
        {

            context.Pilotes.Remove(entity);
        }

        public void Update(Pilote entity)
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
