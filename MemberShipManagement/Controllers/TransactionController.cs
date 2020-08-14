using Postal;
using MemberShipManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Net.Mail;
using System.Data.Entity;

namespace MemberShipManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class TransactionController : Controller
    {
        MembershipManagementDBEntities db = new MembershipManagementDBEntities();
        // GET: Transaction
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
        public bool GetDueStatus(int Id, DateTime date)
        {
            bool status = false;
            var obj = db.Transactions.Where(x => x.PaidBy_Id == Id && x.Billing_Date.Month == date.Month && x.Billing_Date.Year == date.Year).FirstOrDefault();
            if (obj != null)
            {
                status = true;
            }

            return status;
        }

        public decimal? GetMonthlyAmountToPayment(int Id, DateTime date)
        {
            decimal? sum = 0;
            var obj = db.Transactions
            .Where(x => x.PaidBy_Id == Id && x.Billing_Date.Month == date.Month && x.Billing_Date.Year == date.Year).ToList();
            foreach (var item in obj)
            {
                sum = item.MonthlyFee;
            }
            //decimal feeBill = 0;
            //feeBill = db.Members.Where(x => x.Id == Id).Select(s => s.PaymentFee).FirstOrDefault();
            //decimal sumSecond = 0;
            //sumSecond = feeBill;
            return sum;
        }
        public decimal? GetMonthlyServiceAmountToPayment(int Id, DateTime date)
        {
            decimal? sum = 0;
            var obj = db.MemberServiceBills.Where(x => x.MemberService_Id == Id && x.BillingDate.Month == date.Month && x.BillingDate.Year == date.Year);
            sum = obj.Select(x => x.BillToPay).FirstOrDefault();
            return sum;
        }
        public int GetTrasnsactionId(int Id, DateTime date)
        {
            int TransactionId = 0;
            var obj = db.Transactions
            .Where(x => x.PaidBy_Id == Id && x.Billing_Date.Month == date.Month && x.Billing_Date.Year == date.Year).ToList();
            foreach (var item in obj)
            {
                TransactionId = item.Id;
            }
            //decimal feeBill = 0;
            //feeBill = db.Members.Where(x => x.Id == Id).Select(s => s.PaymentFee).FirstOrDefault();
            //decimal sumSecond = 0;
            //sumSecond = feeBill;
            return TransactionId;
        }

        public decimal? GetMonthlyPaidAmount(int Id, DateTime date)
        {
            decimal sum = 0;
            var obj = db.Transactions
            .Where(x => x.PaidBy_Id == Id && x.Billing_Date.Month == date.Month && x.Billing_Date.Year == date.Year).ToList();
            foreach (var item in obj)
            {
                sum = item.AmountPaid;
            }
            //decimal feeBill = 0;
            //feeBill = db.Members.Where(x => x.Id == Id).Select(s => s.PaymentFee).FirstOrDefault();
            //decimal sumSecond = 0;
            //sumSecond = feeBill;
            return sum;
        }
        public decimal? GetMonthlyServicePaidAmount(int Id, DateTime date)
        {
            decimal? sum = 0;
            var obj = db.MemberServiceBills.Where(x => x.MemberService_Id == Id && x.BillingDate.Month == date.Month && x.BillingDate.Year == date.Year);
            sum = obj.Select(x=>x.AmountPaid).FirstOrDefault();
            return sum;
        }
        string Message = "Trasnsaction Failed";

        public JsonResult GetTransactionList(TransactionModel model)
        {
            List<Member> members = db.Members.Where(x => (x.Created_Date.Value.Month <= model.Billing_Date.Month && x.Created_Date.Value.Year <= model.Billing_Date.Year && x.Organization_Id == model.Organization_Id) || (x.Created_Date.Value.Month >= model.Billing_Date.Month && x.Created_Date.Value.Year < model.Billing_Date.Year && x.Organization_Id == model.Organization_Id)).ToList();
            List<TransactionModel> vm = members.Select(x => new TransactionModel
            {
                MemberName = x.Name,
                Member_Id = x.Id,
                Email = x.Email,
                Gender = x.Gender,
                Phone = x.Phone,
                PaymentFee = x.PaymentFee,
                TotalPayment = GetTotalAmount(x.Id),
                MemberCreatedDate = x.Created_Date,
                MemberStatus = x.Status.GetValueOrDefault(),
                Organization_Id = x.Organization_Id,
                LastPaidAmount = x.Transactions.Select(s => s.AmountPaid).LastOrDefault(),
                GetMonthlyPayment = GetMonthlyAmountToPayment(x.Id, model.Billing_Date),
                GetDueAmount = GetMonthlyAmountToPayment(x.Id, model.Billing_Date) - GetMonthlyPaidAmount(x.Id, model.Billing_Date),
                Id = GetTrasnsactionId(x.Id, model.Billing_Date),
                TransactionStatus = GetDueStatus(x.Id, model.Billing_Date),
                PaidBy_Id = x.Transactions.Select(s => s.PaidBy_Id).LastOrDefault(),
                LastTransactionDate = x.Transactions.Select(s => s.Received_Date).LastOrDefault(),
                Billing_Date = x.Transactions.Where(s=>s.Billing_Date == model.Billing_Date).Select(s => s.Billing_Date).FirstOrDefault()

            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult GetServiceBillList(ServiceBillModel model)
        {
            var members = db.Member_Service.Where(x => x.Service_Id == model.ServiceId && x.Status == model.Status && x.Member.Status == model.MemberStatus && x.Member.Organization_Id == model.Organization_Id).ToList();
            List<ServiceBillModel> vm = members.Select(x => new ServiceBillModel
            {
                MemberName = x.Member.Name,
                Member_Id = x.Member_Id,
                Email = x.Member.Email,
                Phone = x.Member.Phone,
                GetAmountToPay = GetMonthlyServiceAmountToPayment(x.Id,model.Billing_Date),
                GetDueAmount = GetMonthlyServicePaidAmount(x.Id,model.Billing_Date),
                Id= x.Id,
                Billing_Date= model.Billing_Date
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPersonTransaction(TransactionModel model)
        {
            List<Transaction> transactions = db.Transactions.Where(x => x.PaidBy_Id == model.PaidBy_Id).ToList();
            List<TransactionModel> vm = transactions.Select(x => new TransactionModel
            {
                Id = x.Id,
                MemberName = x.Member.Name,
                Billing_Date = x.Billing_Date,
                UserName = x.User.Name,
                Amount = x.AmountPaid,
                TotalPayment = GetTotalAmount(x.PaidBy_Id)
            }).ToList();
            return Json(vm, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveServiceBill(ServiceBillModel model)
        {
            var objUser = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            var findExistanceOfBill = db.MemberServiceBills.Where(x => x.MemberService_Id == model.Id && x.BillingDate == model.Billing_Date && x.BillToPay > 0).FirstOrDefault();

            if (findExistanceOfBill != null)
            {
                MemberServiceBill updateMemberServiceBillDB = db.MemberServiceBills.Find(findExistanceOfBill.Id);
                updateMemberServiceBillDB.Received_Date = model.Received_Date;
                updateMemberServiceBillDB.ReceivedBy_Id = objUser.Id;
                updateMemberServiceBillDB.BillToPay = model.Amount;
                updateMemberServiceBillDB.AmountPaid = model.GetDueAmount;
                db.Entry(updateMemberServiceBillDB).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                MemberServiceBill memberServiceBillDB = new MemberServiceBill();
                memberServiceBillDB.MemberService_Id = model.Id;
                memberServiceBillDB.BillToPay = model.Amount;
                memberServiceBillDB.AmountPaid = model.GetDueAmount;
                memberServiceBillDB.BillingDate = model.Billing_Date;
                memberServiceBillDB.Status = true;
                if (memberServiceBillDB.AmountPaid>0)
                {
                    memberServiceBillDB.Received_Date = DateTime.Now;
                    memberServiceBillDB.ReceivedBy_Id = objUser.Id;
                    db.MemberServiceBills.Add(memberServiceBillDB);
                    db.SaveChanges();
                }
                else
                {
                    db.MemberServiceBills.Add(memberServiceBillDB);
                    db.SaveChanges();
                }
                
            }
            return Json(JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveTransaction(TransactionModel model)
        {
           
            if (model.Id > 0)
            {
                var obj = db.Members.Where(x => x.Id == model.PaidBy_Id).FirstOrDefault();
                var objUser = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                DateTime dateObj = DateTime.Now;
                Transaction transDB = db.Transactions.Find(model.Id);
                transDB.AmountPaid = model.Amount;
                db.Entry(transDB).State = EntityState.Modified;
                db.SaveChanges();
                Message = "Transaction is Successfull";
            }
            else
            {
                DateTime month = model.BillingFromDate;
                //model.Billing_Date = model.BillingFromDate;
                while (month <= model.BillingToDate)
                {
                    var obj = db.Members.Where(x => x.Id == model.PaidBy_Id).FirstOrDefault();
                    var objUser = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                    DateTime dateObj = DateTime.Now;
                    Transaction transDB = new Transaction();
                    transDB.PaidBy_Id = model.PaidBy_Id;
                    transDB.ReceivedBy_Id = objUser.Id;
                    transDB.Organization_Id = obj.Organization_Id;
                    transDB.Billing_Date = month;
                    transDB.Received_Date = DateTime.Now;
                    transDB.AmountPaid = model.Amount;
                    //transDB.Amount_Id = model.Amount_Id;
                    transDB.MonthlyFee = model.PaymentFee;
                    transDB.Status = true;
                    db.Transactions.Add(transDB);
                    db.SaveChanges();
                    month = month.AddMonths(1);
                }
                
                Message = "Transaction is Successfull";
                try
                {
                    WebMail.SmtpServer = "smtp.gmail.com";
                    //gmail port to send emails  
                    WebMail.SmtpPort = 587;
                    WebMail.SmtpUseDefaultCredentials = true;
                    //sending emails with secure protocol  
                    WebMail.EnableSsl = true;
                    //EmailId used to send emails from application  
                    WebMail.UserName = "pytha.gorasaco@gmail.com";
                    WebMail.Password = "User@123";

                    //Sender email address.  
                    WebMail.From = "pytha.gorasaco@gmail.com";
                    //Send email  
                    WebMail.Send(to: model.Email, subject: "Transaction", body: model.Amount + "BDT Successfully Recieved" + ", Thank You");
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }




            }
            return Json(new { Message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MemberServiceBill()
        {
            return Json(JsonRequestBehavior.AllowGet);
        }
       // public JsonResult GetServiceBillList(ServiceBillModel model)
       // {
       //     List<Member> members = db.Members.Where(x => (x.Created_Date.Value.Month <= model.Billing_Date.Month && x.Created_Date.Value.Year <= model.Billing_Date.Year && x.Organization_Id == model.Organization_Id) || (x.Created_Date.Value.Month >= model.Billing_Date.Month && x.Created_Date.Value.Year < model.Billing_Date.Year && x.Organization_Id == model.Organization_Id)).ToList();
       //     List<ServiceBillModel> vm = members.Select(x => new ServiceBillModel
       //     {
       //         Id = x.Id,
       //         MemberName=x.Name,
       //         Email=x.Email,
       //         Address=x.Address,
       //         Gender=x.Gender,
       //         Organization_Id=x.Organization_Id,
       //         Phone=x.Phone,
       //         ServiceList = GetServiceList(x.Id),
       //     }).ToList();
       //     return Json(vm, JsonRequestBehavior.AllowGet);
       //}

        //public List<ServiceBillModel> GetServiceList(int Id)
        //{
        //    List<ServiceBillModel> vm = db.Member_Service.Where(x => x.Member_Id == Id).Select(x => new ServiceBillModel
        //    {
        //       ServiceId=x.Service_Id,
        //       ServiceName=x.Service.Name
        //    }).ToList();
        //    return vm;
        //}
        //public List<String> GetServiceNameList(int Id)
        //{
        //    var ServiceNameList = db.Member_Service.Where(x => x.Member_Id == Id);
        //    return ;
        //}
        //public JsonResult SaveAmountTo(TransactionModel model)
        //{
        //    PaymentShipAmount payAmount = new PaymentShipAmount();
        //    payAmount.AmountToPay = model.Amount;
        //    payAmount.FromDate = DateTime.Now;
        //    db.PaymentShipAmounts.Add(payAmount);
        //    db.SaveChanges();
        //    Message = "Amount Saved SuccessFully";
        //    return Json(new { Message }, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetAmount()
        //{
        //    var AllAmountToPay = db.PaymentShipAmounts.ToList();
        //    List<TransactionModel> vm = db.PaymentShipAmounts.Select(x => new TransactionModel
        //    {
        //        Id= x.Id,
        //        Amount = x.AmountToPay
        //    }).ToList();
        //    return Json(vm,JsonRequestBehavior.AllowGet);

        //}
    }
}

