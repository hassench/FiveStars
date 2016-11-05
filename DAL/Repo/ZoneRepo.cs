using Entities.Models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL.Repo
{
    //Identity Repository Interface
    public interface IZoneRepository : IGenericRepository<Zone, int>
    {
        Zone GetSingleByName(string nomzone);
    }

    //User Repository
    public class ZoneRepository : IZoneRepository, IDisposable
    {

        public readonly ApplicationDbContext context = new ApplicationDbContext();

        public IList<Zone> GetAll()
        {

            IList<Zone> query = (from c in context.Zones select c).ToList<Zone>();

            return query;
        }

        public Zone GetSingle(int ZoneId)
        {

            var query = (from c in context.Zones select c).FirstOrDefault<Zone>(x => x.ZoneId == ZoneId);
            return query;
        }

        public Zone GetSingleByPilote(Pilote pilote)
        {

            var query = (from c in context.Zones where c.PiloteZoneObli.PiloteId == pilote.PiloteId || c.PiloteZoneOpti.PiloteId == pilote.PiloteId select c).FirstOrDefault<Zone>();
            return query;
        }

        public Zone GetSingleByName(string nomzone)
        {

            var query = (from c in context.Zones select c).FirstOrDefault<Zone>(x => (x.NomZone == nomzone));
            return query;
        }

        public void Add(Zone entity)
        {

            context.Zones.Add(entity);
        }

        public void Delete(Zone entity)
        {

            context.Zones.Remove(entity);
        }

        public void Update(Zone entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            //Zone auditeur = (from c in context.Zones where c.ZoneId == entity.ZoneId select c).Single<Zone>();
            //auditeur.NomZone = entity.NomZone;
            //auditeur.Appartenance = entity.Appartenance;
            //auditeur.ResponsableHierarZone = entity.ResponsableHierarZone;
            //auditeur.PiloteZoneObli = entity.PiloteZoneObli;
            //auditeur.PiloteZoneOpti = entity.PiloteZoneOpti;
            //auditeur.TypeZone = entity.TypeZone;
            //auditeur.DateIntroBaseZone = entity.DateIntroBaseZone;

        }

        public void Save()
        {
            context.SaveChanges();
        }

        void IDisposable.Dispose() { }
        public void Dispose() { context.Dispose(); }

    }
}
