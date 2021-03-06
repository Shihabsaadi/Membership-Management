﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MembershipManagementDBEntities : DbContext
    {
        public MembershipManagementDBEntities()
            : base("name=MembershipManagementDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Committee> Committees { get; set; }
        public virtual DbSet<Committee_Member> Committee_Member { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Task_Member> Task_Member { get; set; }
        public virtual DbSet<TaskAgenda> TaskAgendas { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Upazila> Upazilas { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<CommitteeMemberView> CommitteeMemberViews { get; set; }
        public virtual DbSet<Member_Service> Member_Service { get; set; }
        public virtual DbSet<MemberServiceBill> MemberServiceBills { get; set; }
    }
}
