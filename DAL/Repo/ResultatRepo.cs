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

    //Resultat Repository Interface
    public interface IResultatRepository : IGenericRepository<Resultat, int>
    {
        IList<Resultat> GetAuditResults(Audit audit);
        Resultat GetSingleByAuThPnt(Audit audit, Theme theme, Point point);
    }

    //Resultat Repository
    public class ResultatRepository : IResultatRepository, IDisposable
    {

        public readonly ApplicationDbContext context = new ApplicationDbContext();

        public IList<Resultat> GetAll()
        {

            IList<Resultat> query = (from c in context.Resultas select c).ToList<Resultat>();

            return query;
        }

        public IList<Resultat> GetAuditResults(Audit audit)
        {

            IList<Resultat> query = (from c in context.Resultas where c.audit.AuditId == audit.AuditId select c).ToList<Resultat>();

            return query;
        }

        public Resultat GetSingle(int resultatId)
        {

            var query = (from c in context.Resultas select c).FirstOrDefault<Resultat>(x => x.ResultatId == resultatId);
            return query;
        }

        public Resultat GetSingleByAuThPnt(Audit audit, Theme theme, Point point)
        {

            var query = (from c in context.Resultas select c).FirstOrDefault<Resultat>(x => (x.audit.AuditId == audit.AuditId) && (x.theme.ThemeId == theme.ThemeId) && (x.point.PointID == point.PointID));
            return query;
        }

        public List<Resultat> GetResultsforAudit(Audit audit)
        {

            var query = (from c in context.Resultas where c.audit.AuditId == audit.AuditId select c).ToList<Resultat>();
            return query;
        }

        public void Add(Resultat entity)
        {

            context.Resultas.Add(entity);
        }

        public void Delete(Resultat entity)
        {

            context.Resultas.Remove(entity);
        }

        public void Update(Resultat entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            //Resultat res = (from c in context.Resultas select c).FirstOrDefault<Resultat>(x => x.ResultatId == entity.ResultatId);
            //res.audit = entity.audit;
            //res.theme = entity.theme;
            //res.point = entity.point;
            //res.Note = entity.Note;
            //res.req_comment = entity.req_comment;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        void IDisposable.Dispose() { }
        public void Dispose() { context.Dispose(); }

    }
}
