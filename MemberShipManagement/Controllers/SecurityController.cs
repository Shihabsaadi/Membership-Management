using MemberShipManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemberShipManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SecurityController : Controller
    {
        MembershipManagementDBEntities db = new MembershipManagementDBEntities();

        // GET: Security
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetSecurityUser()
        {
            List<User> users = db.Users.ToList();
            List<UserModel> vm = users.Where(x=>x.Role == "SuperAdmin").Select(x => new UserModel
            {
                Id = x.Id,
                Name = x.Name,
                UserName = x.UserName,
                Status = x.Status
            }).ToList();
            return Json(vm,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDuplicateUserNameValidation(MemberModel model)
        {
            var validate = true;
            if (model.Id > 0)
            {
                User findMember = db.Users.Find(model.Id);
                var userValidation = db.Users.Where(x => x.UserName == model.UserName).FirstOrDefault();
                if (userValidation != null && findMember.UserName != model.UserName)
                {
                    validate = false;
                }
            }
            else
            {
                var userValidation = db.Users.Where(x => x.UserName == model.UserName).ToList();

                if (userValidation.Count > 0)
                {
                    validate = false;
                }
            }

            return Json(validate, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveSecurityUser(UserModel model)
        {
            var Message = "Couldn't operate Successfully";
            var obj = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
           if(obj.Id != model.Id)
            {
                if (model.Id > 0)
                {

                    if (model.Name != null && model.UserName != null)
                    {
                        var findUser = db.Users.Find(model.Id);
                        findUser.Name = model.Name;
                        findUser.UserName = model.UserName;
                        findUser.Status = model.Status;
                        db.Entry(findUser).State = EntityState.Modified;
                        db.SaveChanges();
                        Message = "Updated Successfully";
                    }

                }
                else
                {
                    if (model.Name != null && model.UserName != null && model.Password != null)
                    {
                        User userDB = new User();
                        userDB.Name = model.Name;
                        userDB.UserName = model.UserName;
                        userDB.Password = model.Password;
                        userDB.Status = model.Status;
                        userDB.Role = "SuperAdmin";
                        db.Users.Add(userDB);
                        db.SaveChanges();
                        Message = model.Name + " Added Successfully";
                    }

                }
            }
            
           
            return Json(Message,JsonRequestBehavior.AllowGet);
        }
    }
}