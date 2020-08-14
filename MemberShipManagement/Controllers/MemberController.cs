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
    public class MemberController : Controller
    {
        // GET: Member
        MembershipManagementDBEntities db = new MembershipManagementDBEntities();
        public bool userLogged()
            {

           var  obj = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            if (obj != null)
            {
                return true;
            }
            else return false;
        }

        public ActionResult Index()
        {
            ViewBag.Active = "Active";
            return View();
        }

        public string getDivisonName(int ? Id)
        {
            string divisionName = "N/A";
            var division = db.Divisions.Where(x => x.Id == Id).SingleOrDefault();
            if (division != null)
            {
                divisionName = division.Name;
            }
            return divisionName;
        }
        public string getDistrictName(int? Id)
        {
            string districtName = "N/A";
            var district = db.Districts.Where(x => x.Id == Id).SingleOrDefault();
            if (district != null)
            {
                districtName = district.Name;
            }
            return districtName;
        }
        public string getUpazillaName(int? Id)
        {
            string upazillaName = "N/A";
            var upazilla = db.Upazilas.Where(x => x.Id == Id).SingleOrDefault();
            if (upazilla != null)
            {
                upazillaName = upazilla.Name;
            }
            return upazillaName;
        }

        #region GetAllMembers
        public JsonResult GetAllmembers()
        {
            List<Member> members = db.Members.ToList();
            List<MemberModel> vm = members.Where(s => s.Status == true).Select(x => new MemberModel
            {
                Name = x.Name,
                Id = x.Id,
                Email = x.Email,
                Gender = x.Gender,
                Address = x.Address,
                OrganizationId = x.Organization_Id,
                OrganizationName = x.Organization.Name,
                Division = getDivisonName(x.Division_Id),
                DivisionId = x.Division_Id.GetValueOrDefault(),
                DistrictId = x.District_Id.GetValueOrDefault(),
                UpazillaId = x.Upazilas_Id.GetValueOrDefault(),
                District = getDistrictName(x.District_Id),
                Upazilla = getUpazillaName(x.Upazilas_Id),
                Phone = x.Phone,
                PaymentFee = x.PaymentFee,
                Status = x.Status.GetValueOrDefault()
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMemberList(MemberModel model)
        {
            List<Member> members = db.Members.ToList();
            MemberModel memberVM = new MemberModel();
            List<MemberModel> vm = members.Where(s=>s.Organization_Id==model.OrganizationId).Select(x => new MemberModel
            { Name = x.Name,
              Id = x.Id,
              Email = x.Email,
              Gender= x.Gender,
              Address = x.Address,
              OrganizationId = x.Organization_Id,
              OrganizationName = x.Organization.Name,
              Division = getDivisonName(x.Division_Id),
              DivisionId = x.Division_Id.GetValueOrDefault(),
              DistrictId = x.District_Id.GetValueOrDefault(),
              UpazillaId = x.Upazilas_Id.GetValueOrDefault(),
              District = getDistrictName(x.District_Id),
              Upazilla= getUpazillaName(x.Upazilas_Id),
              Phone=x.Phone,
              PaymentFee = x.PaymentFee,
              Status = x.Status.GetValueOrDefault()
            }).ToList();
            //var obj = db.Members.ToList();
                return Json(vm, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region GetMember
        public JsonResult GetMember(int Id)
        {
            return Json(JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region SaveMember
        public JsonResult SaveMember(MemberModel model)
        {
            var obj = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var message = "Couldn't Added Member Properly";
            if(model.Id>0)
            {
                Member updateDB = db.Members.Find(model.Id);
                if(model.Name!=null && model.Gender!=null && model.Address != null && model.ValidationEmail !=false)
                {
                    if(model.UpazillaId==0)
                    {
                        model.UpazillaId = null;
                    }
                    updateDB.Name = model.Name;
                    updateDB.Email = model.Email;
                    updateDB.Phone = model.Phone;
                    updateDB.Gender = model.Gender;
                    if(updateDB.Id != obj.Member_Id)
                    {
                        updateDB.Status = model.Status;
                    }
                    updateDB.Division_Id = model.DivisionId;
                    updateDB.District_Id = model.DistrictId;
                    updateDB.Upazilas_Id = model.UpazillaId;
                    updateDB.Address = model.Address;
                    updateDB.PaymentFee = model.PaymentFee;
                    db.Entry(updateDB).State = EntityState.Modified;
                    db.SaveChanges();
                    if(updateDB.Status == false)
                    {

                    }
                    message = "Member Updated Successfully";
                }
                else
                {
                    message = "Couldn't Update Properly";
                }
            }
            else
            {
                if (model.Name != null && model.Gender != null && model.Address != null && model.ValidationEmail != false)
                {
                    Member memberDB = new Member();
                    memberDB.Name = model.Name;
                    memberDB.Email = model.Email;
                    memberDB.Phone = model.Phone;
                    memberDB.Gender = model.Gender;
                    memberDB.Status = model.Status;
                    memberDB.Division_Id = model.DivisionId;
                    memberDB.District_Id = model.DistrictId;
                    memberDB.Upazilas_Id = model.UpazillaId;
                    memberDB.Address = model.Address;
                    memberDB.PaymentFee = model.PaymentFee;
                    memberDB.Organization_Id = model.OrganizationId;
                    memberDB.Created_Date = DateTime.Now;
                    db.Members.Add(memberDB);
                    db.SaveChanges();
                    User userDB = new User();
                    userDB.Name = memberDB.Name;
                    userDB.UserName = memberDB.Email;
                    userDB.Password = memberDB.Phone;
                    userDB.Status = true;
                    userDB.Member_Id = memberDB.Id;
                    userDB.Organization_Id = memberDB.Organization_Id;
                    userDB.Role = "Member";
                    db.Users.Add(userDB);
                    db.SaveChanges();
                    message = model.Name+" Added Successfully";
                }
                else
                {
                    
                }
            }
            return Json(new { Message=message } , JsonRequestBehavior.AllowGet);
        }


        #endregion
        public JsonResult InActiveMember(MemberModel model)
        {
            var obj = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var message = "Inactivated";
            Member updateDB = db.Members.Find(model.Id);
            if(obj.Member_Id==updateDB.Id)
            {
               message = "Self Inactivation Isn't Allow";
            }
            else
            {
                updateDB.Status = model.Status;
                db.Entry(updateDB).State = EntityState.Modified;
                db.SaveChanges();
            }
           
            return Json(message,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDivision()
        {
            var divisions = db.Divisions.ToList();
            List<AddressModel> vm = divisions.Select(x => new AddressModel
            {
                DivisionId = x.Id,
                DivisionName = x.Name
            }).ToList();
            return Json(vm,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDistrict(AddressModel model)
        {
            var districts = db.Districts.Where(x => x.Division_Id == model.DivisionId).ToList();
            List<AddressModel> vm = districts.Select(x => new AddressModel
            {
                DistrictId = x.Id,
                DistrictName = x.Name
            }).ToList();
            return Json(vm,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUpazila(AddressModel model)
        {
            var upazilas = db.Upazilas.Where(x=>x.District_Id == model.DistrictId).ToList();
            List<AddressModel> vm = upazilas.Select(x => new AddressModel
            {
                UpazilaId = x.Id,
                UpazailaName = x.Name
            }).ToList();
            return Json(vm,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDuplicateMailValidation(MemberModel model)
        {
            var validate = true;
            if (model.Id>0)
            {
                Member findMember = db.Members.Find(model.Id);
                var mailValidation = db.Members.Where(x => x.Email == model.Email).FirstOrDefault();
                if(mailValidation != null && findMember.Email != model.Email)
                    {
                    validate = false;
                }
            }
            else
            {
                var mailValidation = db.Members.Where(x => x.Email == model.Email).ToList();
               
                if (mailValidation.Count > 0)
                {
                    validate = false;
                }
            }
            
            return Json(validate, JsonRequestBehavior.AllowGet);
        }
    }
}