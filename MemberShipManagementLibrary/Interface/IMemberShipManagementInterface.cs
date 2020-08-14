using MemberShipDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberShipManagementLibrary.Interface
{
   public  interface IMemberShipManagementInterface
    {
        List<MemberDomainModel> GetAllMembers();

    }
}
