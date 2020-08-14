using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberShipManagement.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public int PaidBy_Id { get; set; }
        public int ReceivedBy_Id { get; set; }
        public int Member_Id { get; set; }
        public int Amount_Id { get; set; }
        public DateTime Billing_Date { get; set; }
        public DateTime BillingFromDate { get; set; }
        public DateTime BillingToDate { get; set; }
        public int Organization_Id { get; set; }
        public DateTime Received_Date { get; set; }
        public string MemberName { get; set; }
        public decimal Amount { get; set; }
        public bool MemberStatus { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Nullable<bool> TransactionStatus { get; set; }
        public decimal LastPaidAmount { get; set; }
        public DateTime LastTransactionDate { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal ? GetMonthlyPayment { get; set; }
        public decimal TotalDue { get; set; }
        public decimal? GetAmountToPay { get; set; }
        public decimal ? GetDueAmount { get; set; }
        public DateTime? MemberCreatedDate { get; set; }
        public decimal PaymentFee {get;set;}
        public decimal ? GasBill { get; set; }
        public decimal ? ElectricBill { get; set; }
        public decimal ? OtherService { get;set;}
    }
}