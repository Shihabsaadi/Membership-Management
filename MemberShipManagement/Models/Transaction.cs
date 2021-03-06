//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MemberShipManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public int Id { get; set; }
        public int PaidBy_Id { get; set; }
        public int ReceivedBy_Id { get; set; }
        public int Organization_Id { get; set; }
        public System.DateTime Billing_Date { get; set; }
        public System.DateTime Received_Date { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal MonthlyFee { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual Member Member { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual User User { get; set; }
    }
}
