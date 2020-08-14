using MemberShipManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemberShipManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class UserController : Controller
    {
        MembershipManagementDBEntities db = new MembershipManagementDBEntities();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        //public  int ? getCommitteId (int Id)
        //{
        //    int ? committeeId;
        //    committeeId = db.Users.Where(x => x.Member_Id == Id).Select(s => s.Member_Id).FirstOrDefault();
        //    return committeeId;
        //}
        //public JsonResult GetUser()
        //{
        //    List<UserModel> vm = db.Members.Select(x => new UserModel
        //    {
        //        Id = x.Id,
        //        Name = x.Name,
        //        Committee_Id = getCommitteId(x.Id)

        //    }).ToList();
        //    return Json(vm,JsonRequestBehavior.AllowGet);
        //}
        public JsonResult SaveWebAccess(UserModel model)
        {
            //if(model.Id>0 && model.Committee_Id==null )
            //{
            //    User user = db.Users.Find(model.Id);
            //    db.Users.Remove(user);
            //    db.SaveChanges();
            //}
            //else if (model.Id > 0 && model.Committee_Id != null)
            //{
            //    User updateUser = db.Users.Find(model.Id);
            //    updateUser.Committee_Id = model.Committee_Id;
            //    //db.Committee_Member.Remove(updateCommitteMemDB);
            //    db.Entry(updateUser).State = EntityState.Modified;
            //    db.SaveChanges();
            //}
            //else
            //{
            User updateUser = db.Users.Where(x => x.Member_Id == model.Member_Id).FirstOrDefault();
            if (updateUser != null)
            {
                updateUser.Committee_Id = model.Committee_Id;
                updateUser.Organization_Id = model.Organization_Id;
                updateUser.Role = "Admin";
                //db.Committee_Member.Remove(updateCommitteMemDB);
                db.Entry(updateUser).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                User user = new User();
                user.Name = model.Name;
                user.UserName = model.UserName;
                user.Role = "Admin";
                user.Password = model.Password;
                user.Member_Id = model.Member_Id;
                user.Committee_Id = model.Committee_Id;
                user.Status = true;
                user.Organization_Id = model.Organization_Id;
                db.Users.Add(user);
                db.SaveChanges();
            }

            return Json(JsonRequestBehavior.AllowGet);
        }

    }
}