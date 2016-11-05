using DAL.Repo;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ServiceComment
    {
        static public void addComment(Comment comment)
        {


            using (CommentRepository commentrepo = new CommentRepository())
            {
                commentrepo.context.Entry(comment.user).State = EntityState.Unchanged;
                commentrepo.context.Entry(comment.zone).State = EntityState.Unchanged;
                commentrepo.Add(comment);
                commentrepo.Save();

            }


        }

    }
}
