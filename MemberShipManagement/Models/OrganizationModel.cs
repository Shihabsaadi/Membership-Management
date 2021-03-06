﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberShipManagement.Models
{
    public class OrganizationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy_Id { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy_Id { get; set; }
        public bool Status { get; set; }
    }
}