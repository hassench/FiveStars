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
    public interface IPointRepository : IGenericRepository<Point, int>
    {
        Point GetSingleByName(string nompoint);
    }

    //User Repository
    public class PointRepository : IPointRepository, IDisposable
    {

        public readonly ApplicationDbContext context = new ApplicationDbContext();

        public IList<Point> GetAll()
        {

            IList<Point> query = (from c in context.Points select c).ToList<Point>();

            return query;
        }


        public Point GetSingle(int PointId)
        {

            var query = (from c in context.Points select c).FirstOrDefault<Point>(x => x.PointID == PointId);
            return query;
        }

        public Point GetSingleByName(string nompoint)
        {

            var query = (from c in context.Points select c).FirstOrDefault<Point>(x => (x.NomPoint == nompoint));
            return query;
        }

        public void Add(Point entity)
        {
            context.Points.Add(entity);
            context.Entry(entity.theme).State = EntityState.Unchanged;
            
        }

        public void Delete(Point entity)
        {
            entity.isDeleted=true;
            context.Entry(entity).State = EntityState.Modified;
            //context.Points.Remove(entity);
        }

        public void Update(Point entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            //Point Point = (from c in context.Points where c.PointID == entity.PointID select c).Single<Point>();
            //Point.Coef= entity.Coef;
            //Point.NomPoint = entity.NomPoint;
            //Point.NumPoint = entity.NumPoint;
            //Point.PointID = Point.PointID;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        void IDisposable.Dispose() { }
        public void Dispose() { context.Dispose(); }

    }
}
