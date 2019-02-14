using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Only4agentsWeb.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult LogOn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = "";//_UserService.ValidateUser(model.UserName, SecurityFunction.EncryptString(model.Password));
                if (user != null)
                {
                    
                    var tt = Session["RoleType"].ToString();
                    if (tt == "SuperAdmin")
                    {
                        return Json("superadmin", JsonRequestBehavior.AllowGet);
                    }
                    else if (tt == "Agency")
                    {
                        return Json("agency", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Session["RoleType"] = null;
                        Session["UserId"] = null;
                        return Json("notAllow", JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    Session["RoleType"] = null;
                    Session["UserId"] = null;
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            }
            ModelState.AddModelError("Password", "The user name or password provided is incorrect.");

            // If we got this far, something failed, redisplay form
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();
            var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
            return View(model);

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}