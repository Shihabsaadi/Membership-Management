using MemberShipManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MemberShipManagement.Controllers
{
   
    public class AccountController : Controller
    {
        // GET: Account
        MembershipManagementDBEntities db = new MembershipManagementDBEntities();
        [Authorize(Roles = "SuperAdmin,Admin,Member")]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            var objUser = db.Users.Where(x => x.UserName == User.Identity.Name && x.Member.Status !=false).FirstOrDefault();
            if(objUser==null)
            {
                return View();

            }
            else
            {
                if (User.IsInRole("Member"))
                {
                    return RedirectToAction("Index", "MyBill");
                }
                else if (User.IsInRole("SuperAdmin"))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Member");
                }
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(AccountModel model)
        {
            bool isValid = db.Users.Any(x => x.UserName == model.UserName && x.Password == model.Password && x.Status == true && x.Member.Status != false);
            if(isValid)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                if (User.IsInRole("Member"))
                {
                    return RedirectToAction("Index", "MyBill");
                }
                else if(User.IsInRole("SuperAdmin"))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Member");
                }
            }
            ModelState.AddModelError("", "Invalid");
            return View();
        }

        [Authorize(Roles = "SuperAdmin,Admin,Member")]
        public JsonResult SavePassword(AccountModel model)
        {
            var Message = "Old Password Doen't Match";
            var objUser = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            if(objUser.Password == model.Password && model.NewPassword == model.ConfirmPassword)
            {
                objUser.Password = model.NewPassword;
                db.Entry(objUser).State = EntityState.Modified;
                db.SaveChanges();
                Message = "Password Updated SuccessFully";
            }
            

            return Json(Message,JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            var objUser = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            if (objUser!=null)
            {
                FormsAuthentication.SignOut();
            }
            
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        public ActionResult Logouts()
        {
            FormsAuthentication.SignOut();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            return View();
        }
    }
}