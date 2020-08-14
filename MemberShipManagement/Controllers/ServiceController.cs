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
    public class ServiceController : Controller
    {
        MembershipManagementDBEntities db = new MembershipManagementDBEntities();

        // GET: Service
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetServiceList(ServiceModel model)
        {
            
            if(model.Status == null)
            {
                List<Service> service = db.Services.ToList();
               var vm = service.Select(x => new ServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Status = x.Status
                }).ToList();
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Service> service = db.Services.ToList();
                var vm = service.Where(x=>x.Status== model.Status).Select(x => new ServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Status = x.Status
                }).ToList();
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
            
           
        }
        public JsonResult SaveService(ServiceModel model)
        {
            var Message = "Action Failed";
            if (model.Id > 0)
            {
                Service updateDB = db.Services.Find(model.Id);
                updateDB.Name = model.Name;
                updateDB.Status = model.Status;
                db.Entry(updateDB).State = EntityState.Modified;
                db.SaveChanges();
                Message = model.Name + " Updated Successfully";
            }
            else
            {
                Service serviceDB = new Service();
                serviceDB.Name = model.Name;
                serviceDB.Status = true;
                db.Services.Add(serviceDB);
                db.SaveChanges();
                Message = model.Name + " Added Successfully";
            }
            return Json(new { Message = Message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMemberList(MemberModel model)
        {
            List<Member> members = db.Members.ToList();
            MemberModel memberVM = new MemberModel();
            List<MemberModel> vm = members.Where(s => s.Organization_Id == model.OrganizationId).Select(x => new MemberModel
            {
                Name = x.Name,
                Id = x.Id,
                Email = x.Email,
                Gender = x.Gender,
                Address = x.Address,
                OrganizationId = x.Organization_Id,
                OrganizationName = x.Organization.Name,
                Phone = x.Phone,
                ServiceName = GetServiceNameList(x.Id),
                Status = x.Status.GetValueOrDefault()
            }).ToList();
            //var obj = db.Members.ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetServiceListToAccess(ServiceModel model)
        {
            List<Service> service = db.Services.ToList();
            List<ServiceModel> vm = service.Select(x => new ServiceModel
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status,
                HasAccess = GetServiceAccessValidation(x.Id,model.MemberId)
            }).ToList();
            return Json(vm,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetServiceHolder(ServiceModel modal)
        {
            var obj = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var vm = db.MemberServiceBills.Where(x => x.Member_Service.Status == true && x.Member_Service.Member.Status == true).Select(x => new ServiceBillModel
            {
                Id= x.Id,
                ServiceName = x.Member_Service.Service.Name,
                Member_Id = x.Member_Service.Member.Id,
                MemberName = x.Member_Service.Member.Name,
                Phone = x.Member_Service.Member.Phone,
                Address= x.Member_Service.Member.Address,
                GetAmountToPay = x.BillToPay,
                GetDueAmount = x.AmountPaid,
                Billing_Date = x.BillingDate,
                Received_Date=x.Received_Date,
                ReceivedBy_Id=obj.Id,
                PaidBy_Id=x.Member_Service.Member.Id
            }).ToList();
            return Json(JsonRequestBehavior.AllowGet);
        }
        public bool GetServiceAccessValidation(int ServiceId,int MemberId)
        {
           bool hasAccess = false;
           var ServiceMember = db.Member_Service.ToList();
            foreach (var item in ServiceMember)
            {
                if(item.Member_Id == MemberId && item.Service_Id == ServiceId && item.Status==true)
                {
                    hasAccess = true;
                }
            }
           return hasAccess;
        }
        public List<String> GetServiceNameList(int Id)
        {
            List<Member_Service> vm = db.Member_Service.Where(x => x.Member_Id == Id).ToList();
            var vms = vm.Select(x => x.Service.Name).ToList();
            return vms;
        }

        public JsonResult SaveAccessForMember(ServiceModel model)
        {
            var obj = db.Member_Service.Where(x => x.Member_Id == model.MemberId && x.Service_Id == model.Id).FirstOrDefault();
            if(obj!=null)
            {
                obj.Status = model.Status;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                Member_Service memberServiceDB = new Member_Service();
                memberServiceDB.Member_Id = model.MemberId;
                memberServiceDB.Service_Id = model.Id;
                memberServiceDB.Status = model.Status;
                db.Member_Service.Add(memberServiceDB);
                db.SaveChanges();
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}