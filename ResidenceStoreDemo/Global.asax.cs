using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ResidenceStoreDemo
{
    using Raven.Client;
    using Raven.Client.Document;
    using Raven.Client.Embedded;
    using ResidenceStore;
    using ResidenceStore.Mailer;
    using ResidenceStore.RavenDB;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Raven
            var store = new EmbeddableDocumentStore() { ConnectionStringName = "ResidenceStoreDemo" };
            store.Initialize();
            DocumentStore = store;

            var mailer = new DelegatedResidenceStoreMailer((email, token, link) => {

            });
            var residenceStore = new RavenResidenceStore(store, mailer);
            ResidenceStore = residenceStore;

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public static RavenResidenceStore ResidenceStore { get; private set; }
        public static IDocumentStore DocumentStore { get; private set; }
    }
}