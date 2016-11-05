using BL.Services;
using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class DelegationController : Controller
    {
        // GET: Delegation
        public ActionResult demanderdelegation([Bind] DelagationViewModel vm)
        {
            Delegation delegation = new Delegation();
            delegation.Delegate   = ServiceAuditeur.GetAuditeur(vm.audit);
            delegation.Concernedaudit = ServiceAudit.getAuditdetached(vm.AuditId);
            delegation.Delegator = ServiceAuditeur.GetAuditeur(ServiceUser.getAuditeurUser(User.Identity.Name));
            delegation.semaine = ServiceSemaine.getCurrentSemaine();
            ServiceDelegation.addDelegation(delegation);
            return RedirectToAction("Index","Auditeur");
        }

        public ActionResult accepterdemandedelegation(int id)
        {
            ServiceDelegation.accepterdemandedelegation(id);
            return RedirectToAction("Index", "Auditeur");
        }

        public ActionResult refuserdemandedelegation(int id)
        {

            ServiceDelegation.refuserdemandedelegation(id);
            return RedirectToAction("Index", "Auditeur");
        }
    }
    
}