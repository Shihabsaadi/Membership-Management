using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MemberShipManagement.Hubs
{
    public class demoHubs : Hub
    {
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<demoHubs>();
            context.Clients.All.displayCustomer();
        }
    }
}
