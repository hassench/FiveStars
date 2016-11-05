using DAL.Repo;
using Entities.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace BL.Services
{
    public class ServicePDCA
    {
        static public List<PDCA> GetPDCAByPilote(string mail)
        {
            int auditeurid;
            using (UserRepository usertrepo = new UserRepository())
            {
                auditeurid = usertrepo.GetPiloteIDBymail(mail);
            }
            List<PDCA> temp;
            using (PDCARepository PdcaRepo = new PDCARepository())
            {
                temp = (List<PDCA>)PdcaRepo.GetByPilote(auditeurid);
            }

            return temp;
        }
        static public List<PDCA> GetPDCAByZone(int id)
        {
            List<PDCA> temp;
            using (PDCARepository PdcaRepo = new PDCARepository())
            {
                temp = (List<PDCA>)PdcaRepo.GetByZone(id);
            }

            return temp;
        }
        static public IList<PDCA> GetPDCAByAudit(int auditId)
        {
            IList<PDCA> PDCAtemp = null;
            using (PDCARepository PDCArepo = new PDCARepository())
            {
                PDCAtemp = PDCArepo.GetByAudit(auditId);
            }

            return PDCAtemp;
        }

        static public PDCA GetPDCA(int PDCAid)
        {
            PDCA PDCAtemp = null;
            using (PDCARepository PDCArepo = new PDCARepository())
            {
                PDCAtemp = PDCArepo.GetSingle(PDCAid);
            }

            return PDCAtemp;
        }

        static public List<PDCA> GetAllPDCAs()
        {
            List<PDCA> PDCAtemp = null;
            using (PDCARepository PDCArepo = new PDCARepository())
            {
                PDCAtemp = (List<PDCA>)PDCArepo.GetAll();
                foreach (PDCA PDCA in PDCAtemp)
                {
                    PDCArepo.context.Entry(PDCA).State = EntityState.Detached;
                }

            }

            return PDCAtemp;
        }

        static public void addPDCA(PDCA PDCA)
        {
            using (PDCARepository PDCArepo = new PDCARepository())
            {
                PDCArepo.context.Entry(PDCA.resultat).State = EntityState.Unchanged;
                PDCArepo.Add(PDCA);
                PDCArepo.Save();
            }
        }

        static public void addPDCAToAudit(Audit audit)
        {
            var resultats= ServiceResultat.getAuditresults(audit);
            foreach (var item in resultats)
            {
                if (item.Note==0)
                {
                    addPDCA(new PDCA { resultat = item });
                }
            }
        }
    }
}