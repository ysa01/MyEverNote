using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        // Temp data ile noteları home /ındexe gönderip orda yakalatmak çin yazılmış bir koddur.
        //public ActionResult Select(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    CategoryManager cm = new CategoryManager();
        //    Category cat = cm.GetCategoryById(id.Value);
        //    if (cat==null)
        //    {
        //        return HttpNotFound();
        //       // return RedirectToAction("Index","Home");  buda olabilir 
        //    }

        //    TempData["mm"] = cat.Notes; // bu güzel bir kod başka actiona göderiyoruz çünkü orda model tanımlı ama kere gidiyor başka yerde olmaz
        //    return RedirectToAction("Index", "Home");
        //}
    }
}