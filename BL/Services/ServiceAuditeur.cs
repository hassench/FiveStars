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
    public class ServiceAuditeur
    {

        static public List<Auditeur> GetAllAuditeurs()
        {
            List<Auditeur> auditeurstemp = null;
            using (AuditeurRepository auditeurrepo = new AuditeurRepository())
            {
                auditeurstemp = (List<Auditeur>)auditeurrepo.GetAll();
                
                foreach (Auditeur auditeur in auditeurstemp)
                {
                    auditeurrepo.context.Entry(auditeur).State = EntityState.Detached;
                }
                
            }

            return auditeurstemp;
        }

        static public List<Auditeur> GetAllAuditeursWdifferentdepartment(Zone zone)
        {
            List<Auditeur> auditeurstemp = null;
            using (AuditeurRepository auditeurrepo = new AuditeurRepository())
            {
                auditeurstemp = (List<Auditeur>)auditeurrepo.GetAlldifferentdepartement(zone);

                foreach (Auditeur auditeur in auditeurstemp)
                {
                    auditeurrepo.context.Entry(auditeur).State = EntityState.Detached;
                }

            }

            return auditeurstemp;
        }

        static public List<Auditeur> GetAllAuditeursWD()
        {
            List<Auditeur> auditeurstemp = null;
            using (AuditeurRepository auditeurrepo = new AuditeurRepository())
            {
                auditeurstemp = (List<Auditeur>)auditeurrepo.GetAll();
            }

            return auditeurstemp;
        }

        static public Auditeur GetAuditeur(int id)
        {
            Auditeur auditeurtemp = null;
            using (AuditeurRepository auditeurrepo = new AuditeurRepository())
            {
                auditeurtemp = auditeurrepo.GetSingle(id);
                auditeurrepo.context.Entry(auditeurtemp).State = EntityState.Detached;
            }
            return auditeurtemp;
        }


        static public List<Auditeur> GetAllAuditeursexceptloggedin(string name)
        {
            List<Auditeur> auditeurstemp = null;
            using (AuditeurRepository auditeurrepo = new AuditeurRepository())
            {
                auditeurstemp = (List<Auditeur>)auditeurrepo.GetAll();

                foreach (Auditeur auditeur in auditeurstemp)
                {
                    auditeurrepo.context.Entry(auditeur).State = EntityState.Detached;
                }

            }

            auditeurstemp = (List<Auditeur>)auditeurstemp.Where(a => a.AuditeurId != ServiceUser.getAuditeurUser(name)).ToList();

            return auditeurstemp;
        }

    }
}
