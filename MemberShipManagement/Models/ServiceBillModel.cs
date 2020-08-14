using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberShipManagement.Models
{
    public class ServiceBillModel
    {
        public int Id { get; set; }
        public int PaidBy_Id { get; set; }
        public int ReceivedBy_Id { get; set; }
        public int Member_Id { get; set; }
        public DateTime Billing_Date { get; set; }
        public int Organization_Id { get; set; }
        public DateTime? Received_Date { get; set; }
        public string MemberName { get; set; }
        public decimal Amount { get; set; }
        public bool MemberStatus { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal LastPaidAmount { get; set; }
        public DateTime LastTransactionDate { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal? GetAmountToPay { get; set; }
        public decimal?  GetDueAmount { get; set; }
        public string ServiceName { get; set; }
        public int ServiceId { get; set; }
        public bool Status { get; set; }
    }
}