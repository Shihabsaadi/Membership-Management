using MemberShipManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemberShipManagement.Controllers
{
   
    public class OrganizationController : Controller
    {
        MembershipManagementDBEntities db = new MembershipManagementDBEntities();
        // GET: Organization
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Index()
        {
            return View();
        }

        #region GetAllOrganizations
        [Authorize(Roles = "SuperAdmin,Admin")]
        public JsonResult GetOrganizationList()
        {
            var objUser = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            if(objUser.Committee_Id==null && objUser.Member_Id==null)
            {
                List<Organization> organizations = db.Organizations.ToList();
                OrganizationModel memberVM = new OrganizationModel();
                List<OrganizationModel> vm = organizations.Select(x => new OrganizationModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Status = x.Status.GetValueOrDefault()
                }).ToList();
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Organization> organizations = db.Organizations.Where(x=>x.Id == objUser.Organization_Id).ToList();
                OrganizationModel memberVM = new OrganizationModel();
                List<OrganizationModel> vm = organizations.Select(x => new OrganizationModel
                {
                    Name = x.Name,
                    Id = x.Id,
                    Status = x.Status.GetValueOrDefault()
                }).ToList();
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
            
           
        }
        #endregion

        #region GetOrganization
        public JsonResult GetOrganization(int Id)
        {
            return Json(JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Save Organization
        [Authorize(Roles = "SuperAdmin")]
        public JsonResult SaveOrganization(OrganizationModel model)
        {
            Organization organizationDB = new Organization();
            var message = "Couldn't Added Organization Properly";
            var obj = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            if (model.Id > 0)
            {
                Organization updateDB = db.Organizations.Find(model.Id);
                if (model.Name != null)
                {
                    updateDB.Name = model.Name;
                    updateDB.Status = model.Status;
                    updateDB.UpdatedBy_Id = obj.Id;
                    db.Entry(updateDB).State = EntityState.Modified;
                    db.SaveChanges();
                    message = "Organization Updated Successfully";
                }
                else
                {
                    message = "Couldn't Update Properly";
                }
            }
            else
            {
                if (model.Name != null )
                {
                    try
                    {
                        if (obj !=null)
                        {
                            organizationDB.Name = model.Name;
                            organizationDB.CreatedBy_Id =  obj.Id;
                            organizationDB.CreatedDate = model.CreatedDate;
                            organizationDB.Status = model.Status;
                            db.Organizations.Add(organizationDB);
                            db.SaveChanges();
                            message = model.Name + " Added Successfully";
                        }
                        
                    }
                    catch(Exception ex)
                    {

                    }
                    
                }
                else
                {

                }
            }
            return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}