using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using ContactAppFinal.Mappers;

namespace ContactAppFinal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.AddProfile<UserProfile>();
            //    cfg.AddProfile<RoleProfile>();
            //    // Add other profiles as needed
            //});
        }
    }
}
