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
    
    public partial class Committee_Member
    {
        public int Id { get; set; }
        public Nullable<int> Committee_Id { get; set; }
        public int Member_Id { get; set; }
        public Nullable<int> Role_Id { get; set; }
        public System.DateTime Accessed_Date { get; set; }
        public int AccessedBy_Id { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual Committee Committee { get; set; }
        public virtual Role Role { get; set; }
        public virtual Member Member { get; set; }
        public virtual User User { get; set; }
    }
}
