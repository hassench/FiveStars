using DAL.Repo;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ServiceUser
    {
        static public ApplicationUser getUser(String name)
        {
            ApplicationUser user;
            using (UserRepository userrepo = new UserRepository())
            {
                user = userrepo.GetSingleByName(name);
                userrepo.context.Entry(user).State = EntityState.Detached;

            }
            return user;
        }

        static public ApplicationUser getUserWD(String name)
        {
            ApplicationUser user;
            using (UserRepository userrepo = new UserRepository())
            {
                user = userrepo.GetSingleByName(name);
                
            }
            return user;
        }
        static public int getAuditeurUser(String name)
        {
            ApplicationUser user;
            int auditeur;
            using (UserRepository userrepo = new UserRepository())
            {
                user = userrepo.GetSingleByName(name);
                auditeur = user.auditeur.AuditeurId;
                userrepo.context.Entry(user).State = EntityState.Detached;

            }
            return auditeur;
        }

        static public ApplicationUser getUserAuditeur(int auditeurid)
        {
            ApplicationUser user;
            using (UserRepository userrepo = new UserRepository())
            {
                user = userrepo.GetSingleByAuditeurID(auditeurid);

            }
            return user;
        }

        static public ApplicationUser getUserPilote(int piloteid)
        {
            ApplicationUser user;
            using (UserRepository userrepo = new UserRepository())
            {
                user = userrepo.GetSingleByPiloteID(piloteid);

            }
            return user;
        }
    }
}
