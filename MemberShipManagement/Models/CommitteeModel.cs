using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberShipManagement.Models
{
    public class CommitteeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime Created_Date { get; set; }
        public System.DateTime Duration_Date { get; set; }
        public int OrganizationId { get; set; }
        public bool Status { get; set; }
    }
}