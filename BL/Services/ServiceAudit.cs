using DAL.Repo;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;

namespace BL.Services
{
    public class ServiceAudit
    {

        static public void addAudit(Audit audit)
        {
            using (AuditRepository auditrepo = new AuditRepository())
            {
                //setting already stored objects to unchanged state so that they dont get readded
                auditrepo.context.Entry(audit.semaine).State = EntityState.Unchanged;
                auditrepo.context.Entry(audit.auditeur).State = EntityState.Unchanged;
                auditrepo.context.Entry(audit.zone).State = EntityState.Unchanged;

                auditrepo.Add(audit);
                auditrepo.Save();

                //setting these objects to Detached state so they dont fuck up the next audit add, u see its very complicated
                //if i dont do this they 'll be tracked with is dbcontext object wich will be disposed of
                auditrepo.context.Entry(audit.semaine).State = EntityState.Detached;
                auditrepo.context.Entry(audit.auditeur).State = EntityState.Detached;
                auditrepo.context.Entry(audit.zone).State = EntityState.Detached;
            }
        }

        static public void updateAudit(Audit audit)
        {
            using (AuditRepository auditrepo = new AuditRepository())
            {
                auditrepo.Update(audit);
                auditrepo.Save();
            }
        }

        static async public Task updatecompletedAudit(int id)
        {
            Audit temp = ServiceAudit.getAuditdetached(id);
            temp.isInProgress = false;
            temp.isCompleted = true;
            temp.FiveStarsLevel = GetAudit5StarsLevel(temp);
            temp.Note = GetAuditNote(temp);
            temp.DateOfCompletion = DateTime.Now;
            ServiceAudit.updateAudit(temp);
            ServicePDCA.addPDCAToAudit(temp);
          await  notifySubmissiontoParticipants(temp);
        }
        static async Task notifySubmissiontoParticipants(Audit audit)
        {
            ApplicationUser auditeuruser=ServiceUser.getUserAuditeur(audit.auditeur.AuditeurId);
            await ServiceEmail.sendEmailAsync(auditeuruser.Email, "Soumission d'audit", "la soumission de l'audit de la zone " + audit.zone.NomZone + " est terminée");

            
            String htmlresultbody = @"";
            htmlresultbody += @"<h2>Dernier Resultats de la Zone " + audit.zone.NomZone + "</h2>";
            List<Theme> themes = ServiceTheme.GetAllThemeswithoutdetaching(audit);
            int compt = -1;
            htmlresultbody += @"<table style=""width: 100%; margin: 0; float: none; text-align:left;"" class=""table"">";
            foreach (Theme item in themes)
            {
                htmlresultbody += @"<tr> <th></th> </tr> <tr> <th></th> <th>" + item.info + @"</th><th></th> <th></th> </tr> <tr> <th></th> </tr>";

                foreach (Point insideitem in item.points)
                {
                    compt++;
                    htmlresultbody += @"<tr> <th style=""width: 5%"">" + insideitem.NumPoint + @"</th> <th style=""width: 63%"">" + insideitem.NomPoint + @"</th> <th style=""width: 7%"">" + audit.resultats[compt].Note + @"</th> <th style=""width: 25%"">" + audit.resultats[compt].req_comment + @"</th> </tr>";
                }
               
            }
            htmlresultbody += @"</table>";//piloteuser.Email
            ApplicationUser piloteuser = ServiceUser.getUserPilote(audit.zone.PiloteZoneObli.PiloteId);
            await ServiceEmail.sendHTMLEmailAsync(piloteuser.Email, "Résultats de l'audit de votre Zone " + audit.zone.NomZone, htmlresultbody);
            if (audit.zone.PiloteZoneOpti!=null)
            {
                ApplicationUser piloteuseropt = ServiceUser.getUserAuditeur(audit.zone.PiloteZoneOpti.PiloteId);
               // await ServiceEmail.sendHTMLEmailAsync(piloteuseropt.Email, "Résultats de l'audit de votre Zone " + audit.zone.NomZone, htmlresultbody);     
            }
        }

        static public int GetAuditNote(Audit audit)
        {
            int auditnote = 0;
            List<Resultat> results = ServiceResultat.getAuditResults(audit);
            foreach (var result in results)
            {
                auditnote += (int)result.Note;
            }
            return auditnote;
        }


        static public int GetAudit5StarsLevel(Audit audit)
        {
            List<Theme> themes = ServiceTheme.GetAllThemeswithoutdetaching();
            List<Resultat> derniersresultats = ServiceResultat.getAuditresults(audit);
            int niv = 0;
            foreach (var theme in themes)
            {
                niv++;
                if (!levelPassed(niv, theme.Passinggrade, derniersresultats, themes))
                {
                    return niv - 1;
                }
            }
            return 5;
        }

        static public bool levelPassed(int niveau, int passinggrade, List<Resultat> res, List<Theme> themes)
        {
            int pointsnumber = 0;
            int pointssum = 0;
            int compt = -1;
            foreach (var theme in themes)
            {
                foreach (var point in theme.points)
                {
                    compt++;
                    if (theme.ThemeId == niveau)
                    {
                        pointsnumber += point.Coef;
                        pointssum += ((int)res[compt].Note) * point.Coef;
                    }
                }
            }
            return (pointssum * 100 / pointsnumber) >= passinggrade;
        }


        static public Audit getAudit(int id)
        {
            Audit temp;
            using (AuditRepository auditrepo = new AuditRepository())
            {
                temp = auditrepo.GetSingle(id);
                auditrepo.Save();
            }
            return temp;
        }

        static public Audit getAuditdetached(int id)
        {
            Audit temp;
            using (AuditRepository auditrepo = new AuditRepository())
            {
                temp = auditrepo.GetSingle(id);
                auditrepo.Save();
                auditrepo.context.Entry(temp).State = EntityState.Detached;
            }
            return temp;
        }

        static public List<Audit> Getallaudits()
        {
            List<Audit> temp;
            using (AuditRepository auditrepo = new AuditRepository())
            {
                temp = (List<Audit>)auditrepo.GetAll();
            }
            return temp;
        }


        static public List<Audit> Getthisweekaudits()
        {
            List<Audit> temp;
            using (AuditRepository auditrepo = new AuditRepository())
            {
                temp = (List<Audit>)auditrepo.GetLatestaudits();
            }
            return temp;
        }

        static public Audit getlatestAuditforZone(int zoneid)
        {
            List<Audit> temp;
            using (AuditRepository auditrepo = new AuditRepository())
            {
                temp = (List<Audit>)auditrepo.GetAll();
            }
            temp = (List<Audit>)temp.Where(c => (c.zone.ZoneId == zoneid) && c.isCompleted).ToList();

            Audit latest = null;
            if (temp.Count > 0)
            {
                latest = temp.MaxBy(t => t.DateOfCompletion);

            }
            return latest;
        }

        static public Audit getbeforelatestAuditforZone(int zoneid)
        {
            List<Audit> temp;
            using (AuditRepository auditrepo = new AuditRepository())
            {
                temp = (List<Audit>)auditrepo.GetAll();
            }
            temp = (List<Audit>)temp.Where(c => (c.zone.ZoneId == zoneid) && c.isCompleted).ToList();

            Audit latest = null;
            if (temp.Count > 1)
            {
                latest = temp.MaxBy(t => t.DateOfCompletion);
                temp.Remove(latest);
                latest = temp.MaxBy(t => t.DateOfCompletion);
            }
            return latest;
        }

        static public List<Audit> getalllatestAuditsforZone(int zoneid)
        {
            List<Audit> temp;
            using (AuditRepository auditrepo = new AuditRepository())
            {
                temp = (List<Audit>)auditrepo.GetAll();
            }
            //taking the audit corresponded to a zone that are completed
            temp = (List<Audit>)temp.Where(c => (c.zone.ZoneId == zoneid) && c.isCompleted).ToList();
            //descending order for these audits by DateOfCompletion 
            temp = (List<Audit>)temp.OrderByDescending(t => t.DateOfCompletion).ToList();
            //getting the top five wich means getting the latest five competed audits for a zone
            //temp = (List<Audit>)temp.Take(5).ToList();

            return temp;
        }

        static public List<Audit> Getmyauditsthisweek(string mail)
        {
            int auditeurid;
            using (UserRepository usertrepo = new UserRepository())
            {
                auditeurid = usertrepo.GetAuditeurIDBymail(mail);
            }
            List<Audit> temp;
            using (AuditRepository auditrepo = new AuditRepository())
            {
                temp = (List<Audit>)auditrepo.GetLatestauditsforUser(auditeurid);
                //foreach (var item in temp)
                //{
                //    auditrepo.context.Entry(item).State = EntityState.Detached;
                //}
            }
            return temp;
        }

        static public List<Audit> Getmymissedauditslastweek(string mail)
        {
            int auditeurid;
            using (UserRepository usertrepo = new UserRepository())
            {
                auditeurid = usertrepo.GetAuditeurIDBymail(mail);
            }
            List<Audit> temp;
            using (AuditRepository auditrepo = new AuditRepository())
            {
                temp = (List<Audit>)auditrepo.GetLastWeekmissingauditsforUser(auditeurid);

            }
            return temp;
        }
    }
}
