using MyEvernote.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    /// singleton patern 
   public class RepositoryBase
    {
        protected static DatabaseContext context;
        private static object _lockSync = new object();//bu ekstra bir kod birden fazla parçacıklı(multitired) programlar için   amaç yine tek bir databasecontex üretmek için sağlama alma amacıyla aşağıki lock içine obj türünden nesne tanımlıyoruz
        protected RepositoryBase() // burda bu classın protected constructerını alarak başka yerde new len mesini istemiyoruz sadece mirasla gelir
        {
            context = CreateContex();

            
        }
       private static  DatabaseContext CreateContex() // buda özellikle static yukada _db yide static yaptık başka yerden çağrılmasın diye  database contex yoksa ifle null ise bir tane newli yoruz sonra hep aynı databasecontexi kullanıyoruz bütün tablolar için Evernoteuser, note ,category, comment için
        {
            if(context==null)
            {
                lock(_lockSync)// kitleme
                {
                    if (context == null)
                    {
                        context = new DatabaseContext();
                    }
                }
            }
            return context;
        }

    }
}
