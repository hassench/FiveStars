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
    public interface IPDCARepository : IGenericRepository<PDCA, int>
    {
        //PDCA GetSingleByName(string nomPdca);
        IList<PDCA> GetByAudit(int id);
        IList<PDCA> GetByPilote(int id);
        IList<PDCA> GetByZone(int id);
    }

    //User Repository
    public class PDCARepository : IPDCARepository, IDisposable
    {

        public readonly ApplicationDbContext context = new ApplicationDbContext();

        public IList<PDCA> GetAll()
        {

            IList<PDCA> query = (from c in context.Pdcas select c).ToList<PDCA>();

            return query;
        }


        public PDCA GetSingle(int PDCAId)
        {

            var query = (from c in context.Pdcas select c).FirstOrDefault<PDCA>(x => x.PDCAId == PDCAId);
            return query;
        }

        //public PDCA GetSingleByName(string nomPdca)
        //{

        //    var query = (from c in context.Pdcas select c).FirstOrDefault<PDCA>(x => (x.NomPDCA == nomPdca));
        //    return query;
        //}

        public void Add(PDCA entity)
        {
            context.Entry(entity.resultat).State = EntityState.Unchanged;
            context.Pdcas.Add(entity);
            

        }

        public void Delete(PDCA entity)
        {

            context.Pdcas.Remove(entity);
        }

        public void Update(PDCA entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            //PDCA PDCA = (from c in context.PDCAs where c.PDCAID == entity.PDCAID select c).Single<PDCA>();
            //PDCA.Coef= entity.Coef;
            //PDCA.NomPDCA = entity.NomPDCA;
            //PDCA.NumPDCA = entity.NumPDCA;
            //PDCA.PDCAID = PDCA.PDCAID;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        void IDisposable.Dispose() { }
        public void Dispose() { context.Dispose(); }


        public IList<PDCA> GetByAudit(int id)
        {
            IList<PDCA> query = (from c in context.Pdcas where c.resultat.audit.AuditId==id select c).ToList<PDCA>();
            return query;
        }
        public IList<PDCA> GetByPilote(int id)
        {
            IList<PDCA> query = (from c in context.Pdcas where c.resultat.audit.zone.PiloteZoneObli.PiloteId == id select c).ToList<PDCA>();
            return query;
        }

        public IList<PDCA> GetByZone(int id)
        {
            IList<PDCA> queryZone = (from c in context.Pdcas where c.resultat.audit.zone.ZoneId == id select c).ToList<PDCA>();
            return queryZone;
        }
    }
}
