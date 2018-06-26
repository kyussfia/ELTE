using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace ELTE.TravelAgency
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Alkalmazás indulása.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
