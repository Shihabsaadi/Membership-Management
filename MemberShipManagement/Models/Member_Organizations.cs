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
    
    public partial class Member_Organizations
    {
        public int Id { get; set; }
        public int AccessBy_Id { get; set; }
        public int Organization_Id { get; set; }
        public Nullable<System.DateTime> Access_Date { get; set; }
        public Nullable<System.DateTime> UpdateAccess_Date { get; set; }
        public Nullable<int> UpdateAccessBy_Id { get; set; }
        public int Member_Id { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual Organization Organization { get; set; }
        public virtual User User { get; set; }
        public virtual Member Member { get; set; }
    }
}
