using DAL.Repo;
using BL.Services;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public class Business
    {
        public void Assign()
        {

            Semaine semaineactuelle = ServiceSemaine.getLastSemaine();
            if (!semaineactuelle.isCurrent)
            {
                semaineactuelle.isCurrent = true;
                ServiceSemaine.updateSemaine(semaineactuelle);
            }
            else
            {

                //Getting this week ready this is abdallah
                semaineactuelle = new Semaine();
                semaineactuelle.isCurrent = true;
                semaineactuelle.Datedebut = DateTime.Now;
                semaineactuelle.Datefin = semaineactuelle.Datedebut.AddDays(6);
                ServiceSemaine.addSemaine(semaineactuelle);
            }

            List<Semaine> Allsemaines = ServiceSemaine.getAllsem();
            Allsemaines = Allsemaines.Where(s => s.SemaineId != semaineactuelle.SemaineId).ToList();
            foreach (var item in Allsemaines)
            {
                item.isCurrent = false;
                ServiceSemaine.updateSemaine(item);
            }
            //I'm going to get the number of "zones" and "auditeurs" 
            //and make sure that I don't choose the same person twice 
            //if there is an other zone to audit and there are auditeurs 
            //that were not yet assigned to any zone
            //GG dude 


            //modified by abdallah
            //PS!!!: if i forget to mention it directly always go through the services
            //see the Services folders and u will instantly get it
            //and encapsulate the repository inside a Using
            //so that the repository gets disposed of
            //thats very critical dude to get rid of the DbContext object
            //thats a nasty object man its messes every thing when it gets tangled with its own kind dude
            //They are Jhon Snows they are LONE WOLVES :p

            List<Zone> zones = ServiceZone.GetAllZones();
            int numZones = zones.Count;

            //same here

            List<Auditeur> auditeurs = ServiceAuditeur.GetAllAuditeurs();
            int numAuditeurs = auditeurs.Count;

            List<int> auditeursPris = new List<int>(numAuditeurs);

            for (int i = 0; i < numAuditeurs; i++)
            {
                auditeursPris.Add(0);
            }

            Random rand = new Random();
            int r;
            int numAssignments = 0;


            for (int i = 0; i < numZones; i++)
            {
                if (numAssignments == numAuditeurs)
                {
                    auditeursPris.Clear();
                    for (int j = 0; j < numAuditeurs; j++)
                    {
                        auditeursPris.Add(0);
                    }
                    numAssignments = 0;
                }
                r = rand.Next(0, numAuditeurs);
                while (auditeursPris[r] != 0 && numAssignments < numAuditeurs)
                {
                    r = rand.Next(0, numAuditeurs);
                }
                numAssignments++;
                auditeursPris[r] = 1;

                //Adding Audits. this is abdallah
                Audit audittemp = new Audit();
                audittemp.TypeAudit = "Regulier";
                audittemp.semaine = semaineactuelle;
                audittemp.auditeur = auditeurs[r];
                audittemp.zone = zones[i];
                audittemp.AuditDay = semaineactuelle.Datedebut.AddDays(rand.Next(2, 6));
                audittemp.isInProgress = false;
                audittemp.isCompleted = false;
                ServiceAudit.addAudit(audittemp);

                Console.WriteLine("l'auditeur " + auditeurs[r].Nom + " est affecté à la zone" + zones[i].NomZone);

                //Sending mail to Auditeur
                //ServiceEmail.sendEmail(auditeurs[r].Mail, "Information sur un prochain Audit", " La zone " + zones[i].NomZone + " sera auditée le jour J par l’auditeur " + auditeurs[r].Nom + " " + auditeurs[r].Prenom + ", merci de vous fixer l’heure de l’audit ");

                //sending mail To Audité aka pilote zone its not provided
                //ServiceEmail.sendEmail("no access to company emails", "Information sur un Audit cette semaine", " La zone " + zones[i].NomZone + " sera auditée le jour J par l’auditeur " + auditeurs[r].Nom + " " + auditeurs[r].Prenom + ", merci de vous fixer l’heure de l’audit ");
            }
        }
    }
}
