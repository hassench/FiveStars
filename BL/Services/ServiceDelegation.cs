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
    public class ServiceDelegation
    {
        static public void addDelegation(Delegation delegation)
        {
            using (DelegationRepository delegationrepo = new DelegationRepository())
            {
                delegationrepo.context.Entry(delegation.Concernedaudit).State = EntityState.Unchanged;
                delegationrepo.context.Entry(delegation.Delegate).State = EntityState.Unchanged;
                delegationrepo.context.Entry(delegation.Delegator).State = EntityState.Unchanged;
                delegationrepo.context.Entry(delegation.semaine).State = EntityState.Unchanged;
                delegationrepo.Add(delegation);
                delegationrepo.Save();
            }
        }

        static public List<Delegation> getDemandesDelegation(int auditid)
        {
            List<Delegation> demandes;
            using (DelegationRepository delegationrepo = new DelegationRepository())
            {
                demandes = (List<Delegation>)delegationrepo.GetAllwithdemands(auditid);
            }
            return demandes;
        }

        static public List<Delegation> getoffresDelegation(int auditid)
        {
            List<Delegation> offres;
            using (DelegationRepository delegationrepo = new DelegationRepository())
            {
                offres = (List<Delegation>)delegationrepo.GetAllwithoffers(auditid);
            }
            return offres;
        }

        static public void accepterdemandedelegation(int id)
        {
            Delegation del;
            using (DelegationRepository delegationrepo = new DelegationRepository())
            {
                del = delegationrepo.GetSingle(id);
                del.treated = true;
                del.accepted = true;
                del.Concernedaudit.auditeur = del.Delegate;
             //   delegationrepo.context.Entry(del.Concernedaudit).State = EntityState.Modified;
                delegationrepo.Update(del);
                delegationrepo.Save();
                
            }

           
        }

        static public void refuserdemandedelegation(int id)
        {
            Delegation del;
            using (DelegationRepository delegationrepo = new DelegationRepository())
            {
                del = delegationrepo.GetSingle(id);
                del.treated = true;
                delegationrepo.Update(del);
                delegationrepo.Save();
            }

        }
    }
}
