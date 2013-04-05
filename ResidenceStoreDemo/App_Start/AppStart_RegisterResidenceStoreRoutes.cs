using System.Web.Mvc;
using System.Web.Routing;

[assembly: WebActivator.PostApplicationStartMethod(typeof(ResidenceStoreDemo.AppStart_RegisterResidenceStoreRoutes), "Start")]
 
namespace ResidenceStoreDemo {
    public static class AppStart_RegisterResidenceStoreRoutes {
        public static void Start() {
            // Set everything up with you having to do any work.
            // I'm doing this because it means that
            // your app will just run. You might want to get rid of this 
            // and integrate with your own Global.asax. 
            // It's up to you. 
            RegisterRoutes(RouteTable.Routes);
        }
 
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Residence", // Route name
                "{controller}/{token}", // URL with parameters
                new { controller = "Residence", action = "Index", token = UrlParameter.Optional } // Parameter defaults
            );
        }
    }
}