using System.Web.Mvc;
using System.Web.Routing;

[assembly: WebActivator.PreApplicationStartMethod(typeof(ResidenceStoreDemo.AppStart_RegisterResidenceStoreRoutes), "Start")]
 
namespace ResidenceStoreDemo {
    using ResidenceStore.Web.Mvc;

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
            routes.MapResidenceRoutes(
                "Residence", // base Route name
                "residence", // base URL 
                new { controller = "Residence" } // Parameter defaults.
            );
        }
    }
}