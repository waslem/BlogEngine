using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogEngine.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // new route for categories
            routes.MapRoute(
                name: "Category",
                url: "Category/{category}",
                defaults: new { controller = "Blog", action = "Category" },
                namespaces: new[] { "BlogEngine.Web.Controllers" }
            );

            // new route for blog entries
            routes.MapRoute(
                name: "BlogEntry",
                url: "blogEntry/{id}/{blogEntryName}",
                defaults: new { controller = "Blog", action = "BlogEntry", id = "", blogEntryName = "" },
                namespaces: new[] { "BlogEngine.Web.Controllers" }
            );

            //new route for comment with optional parent
            routes.MapRoute(
                name: "comment",
                url: "blog/comment/{id}/{parent}",
                defaults: new { controller = "blog", action = "comment", id = "", parent = UrlParameter.Optional },
                namespaces: new[] { "BlogEngine.Web.Controllers" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }, 
                namespaces: new[] { "BlogEngine.Web.Controllers" }
            );

        }
    }
}