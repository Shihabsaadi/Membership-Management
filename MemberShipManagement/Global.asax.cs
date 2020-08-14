using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MemberShipManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //GlobalFilters.Filters.Add(new AuthorizeAttribute());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //SqlDependency.Start(ConfigurationManager.ConnectionStrings["MembershipManagementDBEntities"].ConnectionString);
        }
        protected void Application_End() 
        {
            SqlDependency.Stop(ConfigurationManager.ConnectionStrings["MembershipManagementDBEntities"].ConnectionString);
        }
    }
}
