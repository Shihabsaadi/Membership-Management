using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberShipManagement.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
        public Nullable<int> Member_Id { get; set; }
        public Nullable<int> Committee_Id { get; set; }
        public Nullable<int> Organization_Id { get; set; }


    }
}