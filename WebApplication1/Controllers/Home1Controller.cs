using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.abc;

namespace WebApplication1.Controllers

{

    public class Home1Controller : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserPassword objUser)
        {
            if (ModelState.IsValid)
            {
                using (abcdEntities db = new abcdEntities())
                {
                    var obj = db.UserPassword.Where(a => a.userName.Equals(objUser.userName) && a.encryptedPassword.Equals(objUser.encryptedPassword)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["ID"] = obj.Id.ToString();
                        Session["userName"] = obj.userName.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["ID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}