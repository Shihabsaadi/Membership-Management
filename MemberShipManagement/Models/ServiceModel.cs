﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberShipManagement.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Status { get; set; }
        public int MemberId { get; set; }
        public bool HasAccess { get; set; }
        public string MemberName { get; set; }
        public string Email { get; set; }
        public bool MemberStatus { get; set; }
    }
}