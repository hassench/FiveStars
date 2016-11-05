using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repo
{
    public interface IGenericRepository<T, K> where T : class // New Repo
    {
        T GetSingle(K Id);
        IList<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        void Save();
    }
}
