using BL.Services;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Reminder
    {
        static public void RemindLateAudit()
        {
            //Getting this week ready this is abdallah
            //Semaine semaineactuelle = ServiceSemaine.addSemaine(semaineactuelle); ;
            List<Audit> nonaudits = ServiceSemaine.getCurrentSemaineWD().audits.Where(a => !a.isCompleted && a.AuditDay < DateTime.Now).ToList();
            foreach (var item in nonaudits)
            {
                ApplicationUser auditeuruser = ServiceUser.getUserAuditeur(item.auditeur.AuditeurId);
                ApplicationUser piloteuser = ServiceUser.getUserPilote(item.zone.PiloteZoneObli.PiloteId);
                ServiceEmail.sendEmail(auditeuruser.Email, "Rappel d'audit", item.AuditDay + " a passé, s'il vous plaît effectuer l'audit de la zone " + item.zone.NomZone);
                ServiceEmail.sendEmail(piloteuser.Email, "Rappel d'audit", item.AuditDay + " a passé, s'il vous plaît effectuer l'audit de la zone " + item.zone.NomZone);
                ApplicationUser optpiloteuser = ServiceUser.getUserPilote(item.zone.PiloteZoneOpti.PiloteId);
                if (optpiloteuser != null)
                {
                    ServiceEmail.sendEmail(optpiloteuser.Email, "Rappel d'audit", item.AuditDay + " a passé, s'il vous plaît effectuer l'audit de la zone " + item.zone.NomZone);
                }
            }
        }
    }
}
