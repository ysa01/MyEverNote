//using MyEvernote.DataAccessLayer.EntityFramework;
//using MyEvernote.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;

//namespace MyEvernote.BusinessLayer
//{
//    public class Test
//    { Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();
//        Repository<Category> repo_category = new Repository<Category>();
//        Repository<Comment> repo_comment = new Repository<Comment>();
//        Repository<Note> repo_note = new Repository<Note>();
//        Repository<Liked> repo_like = new Repository<Liked>();

//        public Test()
//        {
            
//          List<Category> categories=  repo_category.List();
//            //List<Category> categories_filtered = repo_category.List(X => X.Id > 5);
//        }
//        public void InsertTest()
//        {
//            Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();
//            int result = repo_user.Insert(new EvernoteUser()
//            {
//                Name = "deneme",
//                SurName = "soyad",
//                Email = "test@hotmail.com",
//                ActivateGuid = Guid.NewGuid(),
//                IsActive = true,
//                IsAdmin = false,
//                UserName = "tester",
//                Password = "123456",
//                CreatedOn = DateTime.Now.AddHours(1),
//                ModifiedOn = DateTime.Now.AddMinutes(65),
//                ModifiedUsername = "aaasas"
//            });
//        }
//        public void UpdateTest() {
//            EvernoteUser user = repo_user.Find(x => x.UserName == "xxxx");
//            if (user != null)
//            {
//                user.UserName = "xxxxx";
//                int result = repo_user.Update();
//            }

//        }
//       public void DeleteTest()
//        {
//            EvernoteUser user = repo_user.Find(x => x.UserName == "xxxxx");
//            if (user != null)
//            {
//               int result= repo_user.Delete(user);
//            }
//        }
//        //Aşağıdaki kodun çalıması için yani note ve owner verilerinin gelmesi için burdan repositorye giderler devamı repositoryde!!!!
//        public void CommentTest()
//        {
//            EvernoteUser user = repo_user.Find(x => x.Id == 5);
//            Note note = repo_note.Find(x => x.Id == 7);
//            Comment comment = new Comment()
//            {
//                CreatedOn = DateTime.Now,
//                ModifiedOn = DateTime.Now,
//                ModifiedUsername = "YavuzAydin",
//                Text = "Bu bir test için yapılan çalışmadır",
//                Note = note,
//                Owner = user,

//            };
//            repo_comment.Insert(comment);
//        }
//    }
//}
