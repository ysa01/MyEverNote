using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using MyEvernote.Entities.Messages;
using MyEvernote.Entities.ValueObjects;
using MyEvernote.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        private EvernoteUserManager evernoteUserManager = new EvernoteUserManager();
        public ActionResult TestNofity()
        {
          WarningViewModel model = new WarningViewModel();
            {
                model.Items = new List<string>(){"yermisin","yemezmisin"};
                model.Header = "olur böyle";
                model.RedirectingTimeout = 100000;
            };
            return View("Warning",model);
        }
        // GET: Home
        public ActionResult Index() {
            // (     bunları test için kulandım
            //    BusinessLayer.Test test = new BusinessLayer.Test();
            //    test.InsertTest();
            //    test.UpdateTest();
            //    test.DeleteTest();
            //    test.CommentTest();  )

            //  (     if (TempData["mm"]!= null)     // bu kodla tempdatadan gelen notları yakalayıp ındexte listeletiyoruz
            //{
            //    // bu bir yöntem tepdata ile index sayfasında seçtiğimiz kategorinin altındaki notları görürüz
            //    return View(TempData["mm"] as List<Note>);

            //}   )
            NoteManager mn = new NoteManager();
            return View(mn.GetAllNote().OrderByDescending(x=>x.ModifiedOn).ToList());
            //  return View(mn.GetAllNoteQueryable().OrderByDescending(x => x.ModifiedOn).ToList());  burda  GetAllNoteQueryable() metoduyla ıqueryable listesi dönüyor descending atarak sql den büyükten küçüğe sıralanarak veri çeker yani tarihi büyük olan yani son yazılanlar sayfada önce gözükür
        }
        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryManager cm = new CategoryManager();
            Category cat = cm.GetCategoryById(id.Value);
            if (cat == null)
            {
                return HttpNotFound();
                // return RedirectToAction("Index","Home");  buda olabilir 
            }

           
            return View("Index", cat.Notes.OrderByDescending(x => x.ModifiedOn).ToList());
        }// bu kod tempdata yapmadan categorileri index syfasındaçağırttık redirectoActionla sayfayı verdik ardından cat.notes notları çağırıp indexte görüntülettirdik
        public ActionResult MostLiked()
        {
            NoteManager mn = new NoteManager();
            return View("Index",mn.GetAllNote().OrderByDescending(x => x.LikeCount).ToList());

        }
        public ActionResult About()
        {
            return View();
        }
        
        public ActionResult ShowProfile()
        {
            EvernoteUser currentUser = Session["login"] as EvernoteUser;
            EvernoteUserManager eum = new EvernoteUserManager();
            BusinessLayerResult<EvernoteUser> res = eum.GetUserById(currentUser.Id);
            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    Items = res.Errors
                };

                return View("Error", errorNotifyObj);
            }

            return View(res.Result);
        }

        public ActionResult EditProfile()
        {

            EvernoteUser currentUser = Session["login"] as EvernoteUser;
            EvernoteUserManager eum = new EvernoteUserManager();
            BusinessLayerResult<EvernoteUser> res = eum.GetUserById(currentUser.Id);
            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    Items = res.Errors
                };

                return View("Error", errorNotifyObj);
            }

            return View(res.Result);
        }
    [HttpPost]
        public ActionResult EditProfile(EvernoteUser model, HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                if (ProfileImage != null &&
                    (ProfileImage.ContentType == "image/jpeg" ||
                    ProfileImage.ContentType == "image/jpg" ||
                    ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";

                    ProfileImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    model.ProfileImageFilename = filename;
                }
                EvernoteUser currentUser = Session["login"] as EvernoteUser;
               
                BusinessLayerResult<EvernoteUser> res = evernoteUserManager.UpdateProfile(model);

                if (res.Errors.Count > 0)
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "Profil Güncellenemedi.",
                        RedirectingUrl = "/Home/EditProfile"
                    };

                    return View("Error", errorNotifyObj);
                }

                // Profil güncellendiği için session güncellendi.
                Session["login"] = res.Result;
                //CurrentSession.Set<EvernoteUser>("login", res.Result);

                return RedirectToAction("ShowProfile");
            }

            return View(model);
        }

        public ActionResult DeleteProfile()
        {
            EvernoteUser currentUser = Session["login"] as EvernoteUser;
            BusinessLayerResult<EvernoteUser> res =
                evernoteUserManager.RemoveUserById(currentUser.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Items = res.Errors,
                    Title = "Profil Silinemedi.",
                    RedirectingUrl = "/Home/ShowProfile"
                };

                return View("Error", errorNotifyObj);
            }

            Session.Clear();

            return RedirectToAction("Index");
        }










        public ActionResult Login()
        {// session ile kullanıcı bilgisi saklama
            // giriş kontrolü ve yönlendirme
            return View(); 
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {


            if (ModelState.IsValid)
            {

                EvernoteUserManager eum = new EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> res = eum.LoginUser(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));//176.derste bu koda ek olarak kullanıcıa hesap aktive ettirmek için link verdik daha sonra yapacağım
                    if(res.Errors.Find(x => x.Code == ErrorMessageCode.UserIsNotActive)!=null)
                    {
                        ViewBag.SetLink = "https://www.google.com.tr/";

                    }
                    return View(model);
                }
                else
                {
                    Session["login"] = res.Result; //session ile kullanıcı tutma
                    return RedirectToAction("Index");//yönlendirme
                }
            }
                return View(model);
        }
        public ActionResult Register()
        { 
            return View(); 
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        { // kullanıcı username kontrolü
          // kullanıcı eposta kontrolü
          //kullanıcı kaydı
          //Aktivasyon eposta göstergesi
            if (ModelState.IsValid)
            {

                EvernoteUserManager eum = new EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> res = eum.RegisterUser(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message)); //burda amaç hangi hata mesajı verdiysek ona göre bir string atamak istersek yani avtive etme linki yada şifremi unuttum gibi hata messajına id eklemiş olduk 
                    return View(model);
                }

                //EvernoteUser user = null;
                //try
                //{
                //   user= eum.RegisterUser(model);
                //}
                //catch (Exception ex)
                //{


                //    ModelState.AddModelError("", ex.Message);
                //}
                //if(user==null)
                //{
                //    return View(model);
                //}

                OkViewModel notifyObj = new OkViewModel()
                {
                    Title = "Kayıt Başarılı",
                    RedirectingUrl = "/Home/Login",
                };

                notifyObj.Items.Add("Lütfen e-posta adresinize gönderdiğimiz aktivasyon link'ine tıklayarak hesabınızı aktive ediniz. Hesabınızı aktive etmeden not ekleyemez ve beğenme yapamazsınız.");

                return View("Ok", notifyObj);
            }

            return View(model);
        }
     
        public ActionResult UserActivate(Guid id)
        {
            BusinessLayerResult<EvernoteUser> res = evernoteUserManager.ActivateUser(id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Geçersiz İşlem",
                    Items = res.Errors
                };

                return View("Error", errorNotifyObj);
            }

            OkViewModel okNotifyObj = new OkViewModel()
            {
                Title = "Hesap Aktifleştirildi",
                RedirectingUrl = "/Home/Login"
            };

            okNotifyObj.Items.Add("Hesabınız aktifleştirildi. Artık not paylaşabilir ve beğenme yapabilirsiniz.");

            return View("Ok", okNotifyObj);
        }

        public ActionResult Logout() 
        {
            Session.Clear();

            return RedirectToAction("Index"); 
        }
    }
}