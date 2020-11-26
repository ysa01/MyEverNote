using MyEvernote.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
  public  class Repository<T>:RepositoryBase where T:class
    {// Davam...    altta newlediğimiz DatabaseContex hem note için hem user(owner) için hemde commente insert yapmada kullanıcağımız için
     //  3 tane farklı db nesnesi üretiyor "multi Ichangedatabastecontex üretemezsin" gibi bundan dolayı singleton patern dediğimiz 
     // yolu kullanıyoruz bussinesslayer'a Repositorrybase diye class oluşturduk devamı orda

        //private DatabaseContext db;
        // private DatabaseContext db = new DatabaseContext();  bu koda da gerek kalmadı static olduğu içn direk oluşturduk

        private DbSet<T> _objectSet;
        public Repository()
        {
           // RepositoryBase db = new RepositoryBase();   normalde böyle olmaslı ama static olduğu için newlemeye gerek yok direk db nesnesini Repsitory baseden çekiyoruz
           // db = RepositoryBase.CreateContex(); bundan da kurtuluyoruz direk miras alıcaz db yi cekcez hazır olarak
            _objectSet = context.Set<T>();
        }
        public List<T> List()
        {
            return context.Set<T>().ToList();
        }

        //public IQueryable<T> List(Expression<Func<T, bool>> where)
        //{
        //    return _objectset.Where(where);
        //}
        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }
       
        public int Insert(T obj)
        {
            context.Set<T>().Add(obj);
            return Save();
        }
        public int Update()
        {
            return Save();
        }
        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }
        public int Save()
        {
          return  context.SaveChanges();
        }
        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }

    }
}
