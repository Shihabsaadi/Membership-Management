using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberShipManagement.Models
{
    public class MemberModel
    {
        public int Id { get; set; }
        public long? RId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Upazilla { get; set; }
        public string Address { get; set; }
        public Nullable<int> DivisionId { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> UpazillaId { get; set; }
        public bool Status { get; set; }
        public string RoleName { get; set; }
        public Nullable<int> RoleId { get; set; }
        public int MemberOrganizationId { get; set; }
        public bool MemberOrganizationStatus { get; set; }
        public string OrganizationName { get; set; }
        public Nullable<bool> Validation { get; set; }
        public int SelectedCommittee { get; set; }
        public Nullable<bool> MemberInCommittee { get; set; }
        public Nullable<int> CommitteeId { get; set; }
        public string CommitteName { get; set; }
        public  int CommitteMemberId { get; set; }
        public int OrganizationId { get; set; }
        public bool ? UserStatus { get; set; }
        public decimal PaymentFee { get; set; }
        public int ? UserCommitteeId { get; set; }
        public int ? UserOrganizationId { get; set; }
        public List<string>  ServiceName { get; set; }
        public List<int> ServiceId { get; set; }
        public bool? ValidationEmail { get; set; }

    }
}