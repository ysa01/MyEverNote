using MyEvernote.DataAccessLayer;
using MyEvernote.DataAccessLayer.Abstract;
using MyEvernote.Entities;
using MyEvernoteCommon;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
  public  class Repository<T>:RepositoryBase,IRepository<T> where T:class
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
            return _objectSet.ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }
        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }
       
        public int Insert(T obj)
        {
           
                if(obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                DateTime now = DateTime.Now;
                o.CreatedOn = now;
                    o.ModifiedOn = now;
                o.ModifiedUsername = App.Common.GetCurrentUsername(); //todo : işlem yapan kullanıcı adı yazılmalı
            }
            _objectSet.Add(obj);
            return Save();
        }
        public int Update(T obj)
        {
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
               o.ModifiedOn =DateTime.Now;
                o.ModifiedUsername = App.Common.GetCurrentUsername(); //todo : işlem yapan kullanıcı adı yazılmalı
            }
            return Save();
        }
        public int Delete(T obj)
        {
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                o.ModifiedOn = DateTime.Now;
                o.ModifiedUsername = App.Common.GetCurrentUsername(); //todo : işlem yapan kullanıcı adı yazılmalı
            }
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
