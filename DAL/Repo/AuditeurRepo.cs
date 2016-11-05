using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using DAL.Models;
using System.Data.Entity;

namespace DAL.Repo
{
    //Identity Repository Interface
    public interface IAuditeurRepository : IGenericRepository<Auditeur, int>
    {
        Auditeur GetSingleByName(string nom, string prenom);
        IList<Auditeur> GetAlldifferentdepartement(Zone zone);
    }

    //User Repository
    public class AuditeurRepository : IAuditeurRepository, IDisposable
    {

        public readonly ApplicationDbContext context = new ApplicationDbContext();

        public IList<Auditeur> GetAll()
        {

            IList<Auditeur> query = (from c in context.Auditeurs select c).ToList<Auditeur>();

            return query;
        }
        public IList<Auditeur> GetAlldifferentdepartement( Zone zone)
        {

            IList<Auditeur> query = (from c in context.Auditeurs where c.Affectation!= zone.Appartenance select c).ToList<Auditeur>();

            return query;
        }
        public IList<Auditeur> GetAllCompleted()
        {

            IList<Auditeur> query = (from c in context.Auditeurs select c).ToList<Auditeur>();

            return query;
        }

        public Auditeur GetSingle(int auditeurId)
        {

            var query = (from c in context.Auditeurs select c).FirstOrDefault<Auditeur>(x => x.AuditeurId == auditeurId);
            return query;
        }

       

        public Auditeur GetSingleByName(string nom, string prenom)
        {

            var query = (from c in context.Auditeurs select c).FirstOrDefault<Auditeur>(x => (x.Nom == nom)&&(x.Prenom == prenom));
            return query;
        }

        public void Add(Auditeur entity)
        {

            context.Auditeurs.Add(entity);
        }

        public void Delete(Auditeur entity)
        {

            context.Auditeurs.Remove(entity);
        }

        public void Update(Auditeur entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            //Auditeur auditeur = (from c in context.Auditeurs where c.AuditeurId == entity.AuditeurId select c).Single<Auditeur>();
            //auditeur.Nom = entity.Nom;
            //auditeur.Prenom = entity.Prenom;
            //auditeur.Affectation = entity.Affectation;
            //auditeur.Telephone = entity.Telephone;
            
            //auditeur.DateIntroBaseAuditeur = entity.DateIntroBaseAuditeur;
            
        }

        public void Save()
        {
            context.SaveChanges();
        }

        void IDisposable.Dispose() { }
        public void Dispose() { context.Dispose(); }

    }
}
