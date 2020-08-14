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
    
    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            this.TaskAgendas = new HashSet<TaskAgenda>();
            this.Task_Member = new HashSet<Task_Member>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public System.DateTime Task_Date { get; set; }
        public System.DateTime Created_Date { get; set; }
        public int AssignedBy_Id { get; set; }
        public string Remark { get; set; }
        public string AppointedTo { get; set; }
        public Nullable<bool> Status { get; set; }
        public int Committee_Id { get; set; }
    
        public virtual Committee Committee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskAgenda> TaskAgendas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task_Member> Task_Member { get; set; }
    }
}