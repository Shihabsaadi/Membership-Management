using MemberShipManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemberShipManagement.Controllers
{
    [Authorize(Roles = "Member,Admin")]
    public class MyBillController : Controller
    {
        // GET: MyBill
        MembershipManagementDBEntities db = new MembershipManagementDBEntities();
        public ActionResult Index()
        {
            return View();
        }
        public decimal GetTotalAmount(int Id)
        {
            decimal sum = 0;
            var obj = db.Transactions
            .Where(x => x.PaidBy_Id == Id).ToList();
            foreach (var item in obj)
            {
                sum += item.AmountPaid;
            }
            return sum;
        }
        public JsonResult GetMyBill(MemberModel model)
        {
            var objUser = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            List<Transaction> transactions = db.Transactions.Where(x => x.PaidBy_Id == objUser.Member_Id).ToList();
            List<TransactionModel> vm = transactions.Select(x => new TransactionModel
            {
                Id = x.Id,
                MemberName = x.Member.Name,
                Billing_Date = x.Billing_Date,
                UserName = x.User.Name,
                Amount = x.AmountPaid,
                GetAmountToPay = x.MonthlyFee,
                TotalPayment = GetTotalAmount(x.PaidBy_Id)
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
    }
}