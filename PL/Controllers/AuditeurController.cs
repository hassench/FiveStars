using BL.Services;
using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    [Authorize(Roles = "Auditeur")]
    public class AuditeurController : Controller
    {
        // GET: Auditeur
        public ActionResult Index()
        {
            List<Delegation> demandes = ServiceDelegation.getDemandesDelegation(ServiceUser.getAuditeurUser(User.Identity.Name));
            List<Delegation> offres   = ServiceDelegation.getoffresDelegation(ServiceUser.getAuditeurUser(User.Identity.Name));


            List<Auditeur> auditeurs = ServiceAuditeur.GetAllAuditeurs();// GetAllAuditeursexceptloggedin(User.Identity.Name);

            List<SelectListItem> auditeursselectlist = new List<SelectListItem>();
            foreach (var item in auditeurs.Where(a => a.AuditeurId != ServiceUser.getAuditeurUser(User.Identity.Name)).ToList())
            {
                auditeursselectlist.Add(new SelectListItem { Value = item.AuditeurId.ToString(), Text = item.Nom + " " + item.Prenom });
            }

            List<Audit> thisweekaudits = ServiceAudit.Getmyauditsthisweek(User.Identity.Name);
            List<Audit> lastweekmissingaudits = ServiceAudit.Getmymissedauditslastweek(User.Identity.Name);

            InterfaceAuditeurViewModel vm = new InterfaceAuditeurViewModel { audits = thisweekaudits, auditsderniersemaine = lastweekmissingaudits, auditeursselectlist = auditeursselectlist, offres = offres, demandes = demandes };

            return View(vm);
        }

        // GET: Auditeur
        public ActionResult Resultats(int id)
        {
            Audit currentaudit = ServiceAudit.getAudit(id);

            ResultsViewModel resultats = new ResultsViewModel();
            resultats.audit = currentaudit;
            resultats.themes = ServiceTheme.GetAllThemeswithoutdetaching(currentaudit);

            if (!currentaudit.isInProgress)
            {
                resultats.resultats = new List<Resultat>();
                foreach (var item in resultats.themes)
                {
                    foreach (var inneritem in item.points)
                    {
                        resultats.resultats.Add(new Resultat { theme = item, point = inneritem, audit = currentaudit, Note = 0 });
                    }
                }
            }
            else resultats.resultats = ServiceResultat.getAuditresults(currentaudit);
            return View(resultats);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Resultats([Bind] ResultsViewModel resultvm)
        {

            if (ModelState.IsValid)
            {

                ServiceResultat.HandleResultats(resultvm);

                //when successfull it delivers a full list of zones
                return RedirectToAction("Index");
            }

            return View(resultvm);
        }

        // GET: Auditeur
        public async Task<ActionResult> Soumettre(int id)
        {
            await ServiceAudit.updatecompletedAudit(id);

            return RedirectToAction("Index");
        }
    }

    public class InterfaceAuditeurViewModel
    {
        public List<Audit> audits { get; set; }
        public List<Audit> auditsderniersemaine { get; set; }
        public List<Delegation> offres { get; set; }
        public List<Delegation> demandes { get; set; }
        public List<SelectListItem> auditeursselectlist { get; set; }
        

    }
    public class InterfaceAuditeurActionOptionViewModel
    {
        public Audit audit { get; set; }
        public List<Delegation> demandes { get; set; }
        public List<SelectListItem> auditeursselectlist { get; set; }

    }
}