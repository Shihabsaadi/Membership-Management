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
    
    public partial class Member_Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member_Service()
        {
            this.MemberServiceBills = new HashSet<MemberServiceBill>();
        }
    
        public int Id { get; set; }
        public int Member_Id { get; set; }
        public int Service_Id { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual Member Member { get; set; }
        public virtual Service Service { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MemberServiceBill> MemberServiceBills { get; set; }
    }
}
