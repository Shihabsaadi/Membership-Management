using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberShipManagement.Models
{
    public class AddressModel
    {
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public int UpazilaId { get; set; }
        public string DivisionName { get; set; }
        public string DistrictName {get;set;}
        public string UpazailaName { get; set; }
    }
}