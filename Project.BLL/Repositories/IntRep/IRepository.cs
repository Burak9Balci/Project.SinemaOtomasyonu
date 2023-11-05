using Project.DAL.Context;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Repositories.IntRep
{
    
    public interface IRepository<T> where T : BaseEntity
    {
        void Save();
        void Add(T item);
        void AddRange(List<T> list);
        void Update(T item);
        void UpdateRange(List<T> list); 
        /// <summary>
        /// Pasife ceker
        /// </summary>
        /// <param name="item"></param>
        void Delete(T item);
        /// <summary>
        /// List Pasife Cekmek
        /// </summary>
        /// <param name="list"></param>
        void DeleteRange(List<T> list); 

        List<T> GetAll();
        List<T> GetPassives();
        List<T> GetInserteds();
        List<T> GetModifieds();
        List<T> GetNotPassives();

        T Find(int id);
        List<T> GetFirstDatas(int number);
        List<T> GetLastDatas(int number);
        List<T> GetCountedDatas(int number);

        bool Any(Expression<Func<T,bool>> exp);
        IQueryable<X> Select<X>(Expression<Func<T,X>>exp);
        T FirstOrDefault(Expression<Func<T,bool>>exp);
        List<T> Where(Expression<Func<T, bool>> exp);

    }
}
