using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace HocTrucTuyen
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "result",
                url: "ketqua/{result}/{count}",
                defaults: new { controller = "Test", action = "Point", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "HocTrucTuyen.Controllers" }
            );
            routes.MapRoute(
              name: "",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
              new[] { "HocTrucTuyen.Controllers" }
          );
            routes.MapRoute(
               name: "Contact",
               url: "lienhe",
               defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
               new[] { "HocTrucTuyen.Controllers" }
           );
            routes.MapRoute(
               name: "Subject",
               url: "KH",
               defaults: new { controller = "Subject", action = "Index", id = UrlParameter.Optional },
               new[] { "HocTrucTuyen.Controllers" }
           );
           
            routes.MapRoute(
                name: "KetQuaTimKiem",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Timkiem", action = "Search", id = UrlParameter.Optional },
                 new[] { "HocTrucTuyen.Controllers" }
            );
        }
    }
}
