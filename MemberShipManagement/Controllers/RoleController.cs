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
    public class RoleController : Controller
    {
        MembershipManagementDBEntities db = new MembershipManagementDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetRoleList()
        {
            List<Role> role = db.Roles.ToList();
            List<RoleModel> vm = role.Select(x => new RoleModel
            {
                Name = x.Name,
                Id = x.Id,
                Status = x.Status
            }).ToList();
            return Json(vm,JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveRole(RoleModel model)
        {
            var Message = "Action Failed";
            if (model.Id > 0)
            {
                Role updateDB = db.Roles.Find(model.Id);
                updateDB.Name = model.Name;
                updateDB.Status = model.Status;
                db.Entry(updateDB).State = EntityState.Modified;
                db.SaveChanges();
                Message = model.Name+ " Updated Successfully";
            }
            else
            {
                Role roleDB = new Role();
                roleDB.Name = model.Name;
                roleDB.Status = true;
                db.Roles.Add(roleDB);
                db.SaveChanges();
                Message = model.Name + " Added Successfully";
            }
            return Json(new{Message = Message }, JsonRequestBehavior.AllowGet);
        }
    }
}