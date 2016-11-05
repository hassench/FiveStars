using Entities.Models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MoreLinq;

namespace DAL.Repo
{
    //Identity Repository Interface
    public interface IThemeRepository : IGenericRepository<Theme, int>
    {
        Theme GetSingleByName(string nomtheme);
    }

    //User Repository
    public class ThemeRepository : IThemeRepository, IDisposable
    {

        public readonly ApplicationDbContext context = new ApplicationDbContext();

        public IList<Theme> GetAll()
        {

            IList<Theme> query = (from c in context.Themes select c).Include(o => o.points).ToList<Theme>();

            return query;
        }


        public Theme GetSingle(int ThemeId)
        {

            var query = (from c in context.Themes select c).FirstOrDefault<Theme>(x => x.ThemeId == ThemeId);
            return query;
        }

        public Theme GetSingleByName(string nomtheme)
        {

            var query = (from c in context.Themes select c).Include(o => o.points).FirstOrDefault<Theme>(x => (x.info == nomtheme));
            return query;
        }

        public void Add(Theme entity)
        {
            context.Themes.Add(entity);
        }

        public void Delete(Theme entity)
        {

            context.Themes.Remove(entity);
        }

        public void Update(Theme entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            //Theme Theme = (from c in context.Themes where c.ThemeId == entity.ThemeId select c).Single<Theme>();
            //Theme.info = entity.info;
            //Theme.Passinggrade = entity.Passinggrade;
            //Theme.points = entity.points;
            //Theme.ThemeId = entity.ThemeId;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        void IDisposable.Dispose() { }
        public void Dispose() { context.Dispose(); }

    }
}
