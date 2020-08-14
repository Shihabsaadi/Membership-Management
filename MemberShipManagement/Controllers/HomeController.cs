using MemberShipManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemberShipManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            MembershipManagementDBEntities db = new MembershipManagementDBEntities();
            
            return View();
        }
    }
}