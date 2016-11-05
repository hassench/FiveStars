using DAL.Repo;
using Entities.Models;
using System.Collections.Generic;
using System.Data.Entity;
using MoreLinq;

namespace BL.Services
{
    public class ServiceSemaine
    {

        static public void addSemaine(Semaine semaine)
        {
            using (SemaineRepository semainerepo = new SemaineRepository())
            {
                semainerepo.Add(semaine);
                semainerepo.Save();
                semainerepo.context.Entry(semaine).State = EntityState.Detached;
            }
        }

        static public void updateSemaine(Semaine semaine)
        {
            using (SemaineRepository semainerepo = new SemaineRepository())
            {
                semainerepo.Update(semaine);
                semainerepo.Save();
                semainerepo.context.Entry(semaine).State = EntityState.Detached;
            }
        }

        static public List<Semaine> getAllsem()
        {
            List<Semaine> allsem;
            using (SemaineRepository semainerepo = new SemaineRepository())
            {
                allsem = (List<Semaine>) semainerepo.GetAll();
                foreach (var item in allsem)
                {
                    semainerepo.context.Entry(item).State = EntityState.Detached;
                }
            }
            return allsem;
        }

        static public Semaine getCurrentSemaine()
        {

            //List<Semaine> lists;
            Semaine semaine;
            using (SemaineRepository semainerepo = new SemaineRepository())
            {
                //lists = (List<Semaine>)semainerepo.GetAll();
                //foreach (var item in lists)
                //{
                //    semainerepo.context.Entry(item).State = EntityState.Detached;
                //}
                semaine = semainerepo.GetCurrent();
                semainerepo.context.Entry(semaine).State = EntityState.Detached;
            }

            //return lists.MaxBy(s => s.SemaineId);
            return semaine;

        }

        static public Semaine getCurrentSemaineWD()
        {
            Semaine semaine;
            using (SemaineRepository semainerepo = new SemaineRepository())
            {

                semaine = semainerepo.GetCurrent();
            }
            return semaine;
        }

        static public Semaine getLastSemaine()
        {
            Semaine semaine;
            using (SemaineRepository semainerepo = new SemaineRepository())
            {

                semaine = semainerepo.GetLast();
                semainerepo.context.Entry(semaine).State = EntityState.Detached;
            }
            return semaine;
        }

        static public Semaine getLastSemaineWD()
        {
            Semaine semaine;
            using (SemaineRepository semainerepo = new SemaineRepository())
            {

                semaine = semainerepo.GetLast();
            }
            return semaine;
        }
    }
}
