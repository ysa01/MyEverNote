using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyEvernote.Entities;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    public class MyInitializer:CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            EvernoteUser admin = new EvernoteUser()
            {
                Name = "Yavuz",
                SurName = "Aydın",
                Email = "yavuzselimaydin06@gmail.com",
                ProfileImageFilename = "user_boy.png",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                UserName = "YavuzAydin",
                Password = "123456",
               
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "YavuzAydin"
            };
            EvernoteUser standartUser = new EvernoteUser()
            {
                Name = "Selim",
                SurName = "Aydın",
                Email = "say_01_say@hotmail.com",
                ProfileImageFilename = "user_boy.png",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                UserName = "SelimAydin",
                Password = "123456",
               
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUsername = "YavuzAydin"
            };
            context.EvernoteUsers.Add(admin);
            context.EvernoteUsers.Add(standartUser);
            for (int i = 0; i < 8; i++)
            {
                EvernoteUser user = new EvernoteUser()
                {
                    Name =FakeData.NameData.GetFirstName(),
                    SurName =FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ProfileImageFilename = "user_boy.png",
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    UserName = $"user{i}",
                    Password = "123",
                   
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUsername = $"user{i}",
                };
                context.EvernoteUsers.Add(user);
            }
            context.SaveChanges();

            //userlist for
            List<EvernoteUser> userslist = context.EvernoteUsers.ToList();
            //Adding fakedata categories
            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title=FakeData.PlaceData.GetStreetName(),
                    Description=FakeData.PlaceData.GetAddress(),
                    CreatedOn=DateTime.Now,
                    ModifiedOn=DateTime.Now,
                    ModifiedUsername="YavuzAydin",
                };
                context.Categories.Add(cat);
                //Adding fake notes
                for (int k = 0; k < FakeData.NumberData.GetNumber(5,9); k++)
                {
                    EvernoteUser owner = userslist[FakeData.NumberData.GetNumber(0, userslist.Count - 1)];
                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        
                        IsDraft=false,
                        LikeCount=FakeData.NumberData.GetNumber(1,9),
                        Owner=owner,
                        CreatedOn=FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUsername=owner.UserName,
                    };
                    cat.Notes.Add(note);
                    //Adding fake comments
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3,5); j++)
                    {
                        EvernoteUser comment_owner = userslist[FakeData.NumberData.GetNumber(0, userslist.Count - 1)];
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),

                            Owner = comment_owner,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUsername =comment_owner.UserName,

                        };
                        note.Comments.Add(comment);
                    }
                    //Adding fake likes 
                   
                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser=userslist[m]
                        };
                        note.Likes.Add(liked);
                    }
                }

            }
            context.SaveChanges();
        }
    }
}
