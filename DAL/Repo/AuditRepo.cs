using Entities.Models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MoreLinq;

namespace DAL.Repo
{
    //Identity Repository Interface
    public interface IAuditRepository : IGenericRepository<Audit, int>
    {
        IList<Audit> GetLatestaudits();
        IList<Audit> GetLatestauditsforUser(int auditeurid);
        IList<Audit> GetLastWeekmissingauditsforUser(int auditeurid);
    }

    //User Repository
    public class AuditRepository : IAuditRepository, IDisposable
    {

        public readonly ApplicationDbContext context = new ApplicationDbContext();

        public IList<Audit> GetAll()
        {

            IList<Audit> query = (from c in context.Audits select c).ToList<Audit>();

            return query;
        }

        public IList<Audit> GetLatestaudits()
        {
            

            List<Audit> query = (from c in context.Audits select c).Include(o => o.zone).ToList<Audit>();

            if (query.Count==0)
            {
                return null;    
            }
           // Audit latest = query.MaxBy(i => i.semaine.SemaineId);
            Semaine latest = (from c in context.Semaines select c).FirstOrDefault<Semaine>(x => x.isCurrent == true);
           List<Audit> result = (List<Audit>)query.Where(item => latest.SemaineId == item.semaine.SemaineId).ToList<Audit>();
           
           return result;
        }

        public IList<Audit> GetLatestauditsforUser(int auditeurid)
        {

            List<Audit> query = (from c in context.Audits select c).Include(o => o.zone).ToList<Audit>();

            //Audit latest = query.MaxBy(i => i.semaine.SemaineId);
            Semaine latest = (from c in context.Semaines select c).FirstOrDefault<Semaine>(x => x.isCurrent == true);
            List<Audit> result = query.Where(item => (latest.SemaineId == item.semaine.SemaineId) && (item.auditeur.AuditeurId == auditeurid)).ToList<Audit>();

            return result;
        }

        public IList<Audit> GetLastWeekmissingauditsforUser(int auditeurid)
        {

            List<Audit> query = (from c in context.Audits select c).Include(o => o.zone).ToList<Audit>();

            //Audit latest = query.MaxBy(i => i.semaine.SemaineId);
            Semaine latest = (from c in context.Semaines select c).FirstOrDefault<Semaine>(x => x.isCurrent == true);
            List<Audit> result = query.Where(item => (latest.SemaineId-1 == item.semaine.SemaineId) && (item.auditeur.AuditeurId == auditeurid)&& !item.isCompleted).ToList<Audit>();

            return result;
        }

        public Audit GetSingle(int AuditId)
        {

            var query = (from c in context.Audits select c).Include(a => a.auditeur).FirstOrDefault<Audit>(x => x.AuditId == AuditId);
            return query;
        }
       


        public void Add(Audit entity)
        {
            context.Audits.Add(entity);
        }

        public void Delete(Audit entity)
        {

            context.Audits.Remove(entity);
        }

        public void Update(Audit entity)
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
