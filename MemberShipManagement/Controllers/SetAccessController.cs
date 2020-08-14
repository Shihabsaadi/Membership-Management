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
    public class SetAccessController : Controller
    {
        MembershipManagementDBEntities db = new MembershipManagementDBEntities();
        // GET: SetAccess
        public ActionResult Index()
        {
            return View();
        }
     
        public JsonResult GetMemberListForSetCommittee(MemberModel model)
        {
            
            var MemberDB = db.Members.ToList();
            if (model.Status == true)
            {
                var CommitteeMemberDB = db.CommitteeMemberViews.ToList();
                CommitteeMemberDB = db.CommitteeMemberViews.Where(x => x.Committee_Id == model.SelectedCommittee && x.MemberCommiitteStatus==true && x.Status==true).ToList();
                List<MemberModel> vm = CommitteeMemberDB.Select(x => new MemberModel
                {
                    Name = x.Name,
                    RId = x.RId,
                    Id = x.Id,
                    Address = x.Address,
                    Email = x.Email,
                    Gender = x.Gender,
                    Phone = x.Phone,
                    RoleId = x.Role_Id.GetValueOrDefault(),
                    RoleName = x.RoleName,
                    CommitteeId = x.Committee_Id.GetValueOrDefault(),
                    CommitteName = x.CommitteeName,
                    MemberInCommittee = x.MemberCommiitteStatus.GetValueOrDefault(),
                    CommitteMemberId = x.CommitteMemberId.GetValueOrDefault(),
                    UserName = x.UserName,
                    UserStatus= x.UserStatus,
                    UserCommitteeId=x.UserCommitteId,
                    UserOrganizationId=x.UserOrganizationId
                }).ToList();
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
            else
            {
                
                List<MemberModel> vm = MemberDB.Where(s=>s.Organization_Id == model.OrganizationId && model.SelectedCommittee != 0 && s.Status==true).Select(x => new MemberModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Email = x.Email,
                    Gender = x.Gender,
                    Phone = x.Phone,
                    Validation = ExistMember(model.SelectedCommittee,x.Id)
                }).ToList();
                return Json(vm, JsonRequestBehavior.AllowGet);
            }


        }

        public bool ExistMember(int CommitteeId,int Id)
        {
          var  CommitteeMemberDB = db.CommitteeMemberViews.Where(x => x.Committee_Id == CommitteeId && x.Id == Id).SingleOrDefault();
            if (CommitteeMemberDB != null)
            {
                return true;
            }
            else return false;
        }
        [HttpPost]
        public JsonResult SaveAccess(MemberModel model)
        {
            var Message = "Action Failed";
            var obj = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            if(obj.Member_Id != model.Id)
            {
                if (model.CommitteMemberId > 0 && model.Validation != true)
                {

                    Committee_Member updateCommitteMemDB = db.Committee_Member.Find(model.CommitteMemberId);
                    var modelStatus = model.Status;
                    updateCommitteMemDB.Committee_Id = model.SelectedCommittee;
                    updateCommitteMemDB.Member_Id = model.Id;
                    updateCommitteMemDB.Role_Id = model.RoleId;
                    updateCommitteMemDB.AccessedBy_Id = obj.Id;
                    updateCommitteMemDB.Accessed_Date = DateTime.Now;
                    updateCommitteMemDB.Status = true;
                    //db.Committee_Member.Remove(updateCommitteMemDB);
                    db.Entry(updateCommitteMemDB).State = EntityState.Modified;
                    db.SaveChanges();
                    Message = "Updated SuccessFully";
                }
                else if (model.Validation == true)
                {
                    Committee_Member updateCommitteMemDB = db.Committee_Member.Find(model.CommitteMemberId);
                    db.Committee_Member.Remove(updateCommitteMemDB);
                    db.SaveChanges();
                    User removeUserDB = db.Users.Where(x => x.Member_Id == model.Id).FirstOrDefault();
                    if (removeUserDB != null)
                    {
                        removeUserDB.Role = "Member";
                        removeUserDB.Committee_Id = null;
                        db.Entry(removeUserDB).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    Message = "Updated SuccessFully";
                }
                else
                {
                    Committee_Member committeMemDB = new Committee_Member();
                    committeMemDB.Committee_Id = model.SelectedCommittee;
                    committeMemDB.Member_Id = model.Id;
                    committeMemDB.Role_Id = model.RoleId;
                    committeMemDB.AccessedBy_Id = obj.Id;
                    committeMemDB.Accessed_Date = DateTime.Now;
                    committeMemDB.Status = true;
                    db.Committee_Member.Add(committeMemDB);
                    db.SaveChanges();
                    Message = "Added SuccessFully";

                }
            }
           
            return Json(new {Message}, JsonRequestBehavior.AllowGet);
        }
    }
}