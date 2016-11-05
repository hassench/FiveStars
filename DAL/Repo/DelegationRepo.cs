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
    //Delegation Repository Interface
    public interface IDelegationRepository : IGenericRepository<Delegation, int>
    {
        IList<Delegation> GetAllwithdemands(int auditid);
        IList<Delegation>  GetAllwithoffers(int auditid);
    }
    //Delegation Repository
    public class DelegationRepository : IDelegationRepository, IDisposable
    {
        public readonly ApplicationDbContext context = new ApplicationDbContext();

        public IList<Delegation> GetAll()
        {
            IList<Delegation> query = (from c in context.Delegations select c).ToList<Delegation>();
            return query;
        }

        public IList<Delegation> GetAllwithdemands(int auditid)
        {

            IList<Delegation> query = (from c in context.Delegations.AsNoTracking() where c.Delegator.AuditeurId == auditid && !c.treated select c).Include(a => a.Delegate).Include(a => a.Concernedaudit).ToList<Delegation>();
            return query;
        }

        public IList<Delegation> GetAllwithoffers(int auditid)
        {
            IList<Delegation> query = (from c in context.Delegations.AsNoTracking() where c.Delegate.AuditeurId == auditid && !c.treated select c).Include(a => a.Delegator).Include(a => a.Delegate).Include(a => a.Concernedaudit).ToList<Delegation>();
            return query;
        }

        public Delegation GetSingle(int DelegationId)
        {
            var query = (from c in context.Delegations select c).Include(a => a.Delegator).Include(a => a.Delegate).Include(a => a.Concernedaudit).FirstOrDefault<Delegation>(x => x.DelegationId == DelegationId);
            return query;
        }

        public void Add(Delegation entity)
        {
            context.Delegations.Add(entity);
        }

        public void Delete(Delegation entity)
        {
            context.Delegations.Remove(entity);
        }

        public void Update(Delegation entity)
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