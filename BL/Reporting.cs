using BL.Services;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
namespace BL
{
    public class Reporting
    {
        public static void createReport()
        {
            List<Zone> zones = ServiceZone.GetAllZones();


            // descending order of zones  by level
            List<ZoneWithLevel> ZonesdescLevel = descStars(zones);
            string reportemailbody = "";
            if (ZonesdescLevel.Count != 0)
            {
                reportemailbody = builddescStars(reportemailbody, ZonesdescLevel);
                // descending order of zones  by Note
                List<ZoneWithNote> ZonesdescNote = descNotes(zones);
                reportemailbody = builddescNotes(reportemailbody, ZonesdescNote);
            }
            // ascending order of zones  by Progress
            List<ZoneWithProgress> ZonesascProgress = ascProgress(zones);
            if (ZonesascProgress.Count != 0)
            {
                reportemailbody = buildascProgress(reportemailbody, ZonesascProgress);
            }

            //gettin zones non auditées
            List<Audit> auditsnonaccompli = getzonesnonauditées();
            reportemailbody = buildnonauditées(reportemailbody, auditsnonaccompli);
            if (ZonesdescLevel.Count != 0)
            {
                //getting audieurs to calculate how much they gave points
                List<Auditeur> auditeurs = ServiceAuditeur.GetAllAuditeursWD();
                reportemailbody = buildauditeurpoints(reportemailbody, auditeurs);
            }

            ServiceEmail.sendHTMLEmail("ghd.abdallah@gmail.com", "Report Five Stars", reportemailbody);

        }

        static List<ZoneWithLevel> descStars(List<Zone> zones)
        {
            Audit audit;
            List<ZoneWithLevel> ZWL = new List<ZoneWithLevel>();
            foreach (var item in zones)
            {
                audit = ServiceAudit.getlatestAuditforZone(item.ZoneId);
                if (audit != null)
                {
                    ZWL.Add(new ZoneWithLevel { zone = item, level = audit.FiveStarsLevel });
                }
            }
            return ZWL.OrderByDescending(z => z.level).ToList();
        }

        static List<ZoneWithNote> descNotes(List<Zone> zones)
        {
            Audit audit;
            int pointcount = ServicePoint.GetAllPoints().Count;
            List<ZoneWithNote> ZWN = new List<ZoneWithNote>();

            foreach (var item in zones)
            {
                audit = ServiceAudit.getlatestAuditforZone(item.ZoneId);

                if (audit != null)
                {
                    ZWN.Add(new ZoneWithNote { zone = item, note = audit.Note, pointcount = pointcount });
                }
            }
            return ZWN.OrderByDescending(z => z.note).ToList();
        }

        static List<ZoneWithProgress> ascProgress(List<Zone> zones)
        {
            Audit latestaudit;
            Audit beforelatestaudit;
            List<ZoneWithProgress> ZWP = new List<ZoneWithProgress>();

            foreach (var item in zones)
            {
                latestaudit = ServiceAudit.getlatestAuditforZone(item.ZoneId);
                beforelatestaudit = ServiceAudit.getbeforelatestAuditforZone(item.ZoneId);
                if (beforelatestaudit != null)
                {
                    ZWP.Add(new ZoneWithProgress { zone = item, progress = latestaudit.Note - beforelatestaudit.Note });
                }

            }
            return ZWP.OrderBy(z => z.progress).ToList();
        }

        static List<Audit> getzonesnonauditées()
        {
            List<Audit> audits = ServiceSemaine.getCurrentSemaineWD().audits.Where(a => !a.isCompleted).ToList();

            return audits;
        }

        static string builddescStars(string body, List<ZoneWithLevel> ZWL)
        {
            string temp = body;
            body += @"<h1>Ordre décroissant des zones en termes de Stars attribués<h1/>";


            body += @"<table> <tr> <th> Nom de la Zone </th> <th> Niveau five Stars </th> </tr>";
            foreach (var item in ZWL)
            {
                body += @" <tr> <td> " + item.zone.NomZone + " </td> <td> " + item.level + " </td> </tr>";
            }


            return body += @"</table><hr />";
        }

        static string builddescNotes(string body, List<ZoneWithNote> ZWN)
        {
            string temp = body;
            body += @"<h1>Ordre décroissant des zones en termes de notes <h1/>";
            body += @"<table> <tr> <th> Nom de la Zone </th> <th> Note </th> </tr>";
            foreach (var item in ZWN)
            {
                body += @" <tr> <td> " + item.zone.NomZone + " </td> <td> " + item.note + "/" + item.pointcount + " </td> </tr>";
            }
            return body += @"</table><hr />";
        }

        static string buildascProgress(string body, List<ZoneWithProgress> ZWP)
        {
            string temp = body;
            body += @"<h1>Ordre décroissant des zones en termes du progrès  <h1/>";
            body += @"<table> <tr> <th> Nom de la Zone </th> <th> Progrés </th> </tr>";
            foreach (var item in ZWP)
            {
                body += @" <tr> <td> " + item.zone.NomZone + " </td> <td> " + item.progress + " </td> </tr>";
            }
            return body += @"</table><hr />";
        }

        static string buildnonauditées(string body, List<Audit> audits)
        {
            string temp = body;
            body += @"<h1>Les Zones Non auditées  <h1/>";
            body += @"<table> <tr> <th style=""color:red""> Auditeur </th>  <th> Nom de la Zone </th> <th> Pilote du Zone </th> <th> Pilote du Zone Optionelle</th> </tr>";
            foreach (var item in audits)
            {
                body += @" <tr> <td style=""color:red""> " + item.auditeur.Nom + " " + item.auditeur.Prenom + " </td> <td> " + item.zone.NomZone + " </td> <td> " + item.zone.PiloteZoneObli.NomPilote + " </td><td> " + item.zone.PiloteZoneOpti.NomPilote + " </td> </tr>";
            }
            return body += @"</table><hr />";
        }

        static string buildauditeurpoints(string body, List<Auditeur> auditeurs)
        {
            string temp = body;
            body += @"<h1>Ordre décroissant des auditeurs par points d’affectation des audits  <h1/>";
            body += @"<table> <tr>  <th> Auditeur </th> <th> Nombre des Points </th>  </tr>";
            foreach (var item in auditeurs)
            {
                int nbrpoint = 0;
                foreach (var inneritem in item.audits)
                {
                    nbrpoint += (inneritem.isCompleted) ? 1 : 0;
                }
                body += @" <tr> <td > " + item.Nom + " " + item.Prenom + " </td> <td> " + nbrpoint + " </td>  </tr>";
            }
            return body += @"</table><hr />";
        }
    }

    class ZoneWithLevel
    {
        public Zone zone { get; set; }
        public int level { get; set; }
    }
    class ZoneWithNote
    {
        public Zone zone { get; set; }
        public int note { get; set; }
        public int pointcount { get; set; }
    }

    class ZoneWithProgress
    {
        public Zone zone { get; set; }
        public int progress { get; set; }
    }
}
