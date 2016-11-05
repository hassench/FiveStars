using DAL.Models;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL.Repo
{
    //Identity Repository Interface
    public interface ICommentRepository : IGenericRepository<Comment, int>
    {
       
    }

    //User Repository
    public class CommentRepository : ICommentRepository, IDisposable
    {

        public readonly ApplicationDbContext context = new ApplicationDbContext();

        public IList<Comment> GetAll()
        {

            IList<Comment> query = (from c in context.Comments select c).ToList<Comment>();

            return query;
        }



        public Comment GetSingle(int CommentId)
        {

            var query = (from c in context.Comments select c).FirstOrDefault<Comment>(x => x.CommentId == CommentId);
            return query;
        }



        public void Add(Comment entity)
        {
            context.Comments.Add(entity);
        }

        public void Delete(Comment entity)
        {

            context.Comments.Remove(entity);
        }

        public void Update(Comment entity)
        {
            context.Entry(entity).State = EntityState.Modified;
          
        }

        public void Save()
        {
            context.SaveChanges();
        }

        void IDisposable.Dispose() { }
        public void Dispose() { context.Dispose(); }

    }
}
