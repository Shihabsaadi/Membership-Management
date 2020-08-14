using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberShipManagement.Models
{
    public class MemberOrganizationModel
    {
        public int Id { get; set; }
        public int Member_Id { get; set; }
        public int Organization_Id { get; set; }
        public bool Status { get; set; }
    }
}