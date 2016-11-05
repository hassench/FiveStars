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
    public interface IUserRepository : IGenericRepository<ApplicationUser, string>
    {
        ApplicationUser GetSingleByName(string name);
        int GetAuditeurIDBymail(string mail);
    }

    //User Repository
    public class UserRepository : IUserRepository, IDisposable
    {

        public readonly ApplicationDbContext context = new ApplicationDbContext();

        public IList<ApplicationUser> GetAll()
        {

            IList<ApplicationUser> query = (from c in context.Users select c).ToList<ApplicationUser>();

            return query;
        }

        public ApplicationUser GetSingle(string UserId)
        {

            var query = (from c in context.Users select c).FirstOrDefault<ApplicationUser>(x => x.Id == UserId);
            return query;
        }
        public int GetAuditeurIDBymail(string mail)
        {

            var query = (from c in context.Users select c).FirstOrDefault<ApplicationUser>(x => x.Email == mail);
            return query.auditeur.AuditeurId;
        }
        public int GetPiloteIDBymail(string mail)
        {

            var query = (from c in context.Users select c).FirstOrDefault<ApplicationUser>(x => x.Email == mail);
            return query.pilote.PiloteId;
        }

        public ApplicationUser GetSingleByName(string name)
        {

            var query = (from c in context.Users select c).FirstOrDefault<ApplicationUser>(x => x.UserName == name);
            return query;
        }

        public ApplicationUser GetSingleByAuditeurID(int AuditeurId)
        {

            var query = (from c in context.Users select c).FirstOrDefault<ApplicationUser>(x => x.auditeur.AuditeurId == AuditeurId);
            return query;
        }

        public ApplicationUser GetSingleByPiloteID(int PiloteId)
        {

            var query = (from c in context.Users select c).FirstOrDefault<ApplicationUser>(x => x.pilote.PiloteId == PiloteId);
            return query;
        }

        public void Add(ApplicationUser entity)
        {

            context.Users.Add(entity);
        }

        public void Delete(ApplicationUser entity)
        {

            context.Users.Remove(entity);
        }

        public void Update(ApplicationUser entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            //ApplicationUser user = (from c in context.Users where c.Id == entity.Id select c).Single<ApplicationUser>();
            //user.UserName = entity.UserName;
            
            //user.SecurityStamp = entity.SecurityStamp;
            //user.PasswordHash = entity.PasswordHash;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        void IDisposable.Dispose() { }
        public void Dispose() { context.Dispose(); }

    }
}
