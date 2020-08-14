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
    public class CommitteeController : Controller
    {
        MembershipManagementDBEntities db = new MembershipManagementDBEntities();
        // GET: Committe
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetCommitteeList(CommitteeModel model)
        {
            var objUser = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            if(objUser.Member_Id != null && objUser.Committee_Id!=null)
            {
                List<Committee> committee = db.Committees.ToList();
                List<CommitteeModel> vm = committee.Where(x=>x.Id == objUser.Committee_Id).Select(x => new CommitteeModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Created_Date = x.Created_Date,
                    Duration_Date = x.Duration_Date,
                    Status = x.Status
                }).ToList();
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
            else if(model.OrganizationId != 0)
            {
                List<Committee> committee = db.Committees.Where(x=>x.Organization_Id==model.OrganizationId).ToList();
                List<CommitteeModel> vm = committee.Select(x => new CommitteeModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Created_Date = x.Created_Date,
                    Duration_Date = x.Duration_Date,
                    Status = x.Status
                }).ToList();
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Committee> committee = db.Committees.ToList();
                List<CommitteeModel> vm = committee.Select(x => new CommitteeModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Created_Date = x.Created_Date,
                    Duration_Date = x.Duration_Date,
                    Status = x.Status
                }).ToList();
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
            
        }

        public JsonResult SaveCommittee(CommitteeModel model)
        {
            var Message = "Action Failed";
            if (model.Id > 0)
            {
                Committee updateDB = db.Committees.Find(model.Id);
                updateDB.Organization_Id = model.OrganizationId;
                updateDB.Name = model.Name;
                updateDB.Status = model.Status;
                updateDB.Duration_Date = model.Duration_Date;
                db.Entry(updateDB).State = EntityState.Modified;
                db.SaveChanges();
                Message = model.Name + " Updated Successfully";
            }
            else
            {
                Committee committeeDB = new Committee();
                committeeDB.Organization_Id = model.OrganizationId;
                committeeDB.Name = model.Name;
                committeeDB.Created_Date = DateTime.Now;
                committeeDB.Duration_Date = model.Duration_Date;
                committeeDB.Status = model.Status;
                db.Committees.Add(committeeDB);
                db.SaveChanges();
                Message = model.Name + " Added Successfully";
            }
            return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
        }
    }
}
