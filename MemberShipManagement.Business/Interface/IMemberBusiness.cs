using MemberShipDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberShipManagement.Business
{
   public interface IMemberBusiness
    {
        List<MemberDomainModel> GetAllMembers();
    }
}
