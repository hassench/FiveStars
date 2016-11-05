using DAL.Repo;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace BL.Services
{
    public class ServiceZone
    {
        static public Zone GetZoneByName(string zonename)
        {
            Zone zonetemp = null;
            using (ZoneRepository zonerepo = new ZoneRepository())
            {
                zonetemp = zonerepo.GetSingleByName(zonename);
            }

            return zonetemp;
        }

        static public Zone GetZone(int zoneid)
        {
            Zone zonetemp = null;
            using (ZoneRepository zonerepo = new ZoneRepository())
            {
                zonetemp = zonerepo.GetSingle(zoneid);
            }

            return zonetemp;
        }


        static public Zone GetZoneBypliote(Pilote pilote)
        {
            Zone zonetemp = null;
            using (ZoneRepository zonerepo = new ZoneRepository())
            {
                zonetemp = zonerepo.GetSingleByPilote(pilote);
            }

            return zonetemp;
        }

        static public Zone GetZonedetached(int zoneid)
        {
            Zone zonetemp = null;
            using (ZoneRepository zonerepo = new ZoneRepository())
            {
                zonetemp = zonerepo.GetSingle(zoneid);
                zonerepo.context.Entry(zonetemp).State = EntityState.Detached;
            }

            return zonetemp;
        }

        static public List<Zone> GetAllZones()
        {
            List<Zone> zonetemp = null;
            using (ZoneRepository zonerepo = new ZoneRepository())
            {
                zonetemp = (List<Zone>)zonerepo.GetAll();
                foreach (Zone zone in zonetemp)
                {
                    zonerepo.context.Entry(zone).State = EntityState.Detached;
                }

            }

            return zonetemp;
        }

        static public void addzone(Zone zone)
        {


            using (ZoneRepository zonerepo = new ZoneRepository())
            {
                zonerepo.context.Entry(zone.PiloteZoneObli).State = EntityState.Unchanged;
                if (zone.PiloteZoneOpti != null)
                {
                    zonerepo.context.Entry(zone.PiloteZoneOpti).State = EntityState.Unchanged;
                }
                zonerepo.Add(zone);
                zonerepo.Save();
            }
        }
        static public int? GetZone5StarsLevel(Zone zone)
        {
            //get last audit to calculate its 5StarsLevel
            Audit currentaudit = ServiceAudit.getlatestAuditforZone(zone.ZoneId);
            if (currentaudit == (null))
            {
                return null;
            }
            return (int?)currentaudit.FiveStarsLevel;
        }

        public static String GetZoneNote(Zone zone)
        {
            Audit audit = ServiceAudit.getlatestAuditforZone(zone.ZoneId);

            if (audit ==null)
            {
                return null;
            }
            return audit.Note.ToString() + "/" + ServicePoint.GetAllPointsForAudit(audit).Count.ToString();
        }

    }
}