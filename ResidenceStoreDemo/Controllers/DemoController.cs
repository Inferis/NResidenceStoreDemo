using System.Web.Http;

namespace ResidenceStoreDemo.Controllers
{
    using System.Linq;
    using Raven.Client;
    using ResidenceStore;
    using ResidenceStore.RavenDB;
    using ResidenceStore.Web.Http;

    public class DemoController : ApiController, IResidenceStoreProvider
    {
        private readonly IDocumentStore store;
        private RavenResidenceStore residenceStore;

        public DemoController()
        {
            store = MvcApplication.DocumentStore;
            residenceStore = MvcApplication.ResidenceStore;
        }

        // GET api/demo
        [AuthorizeWithResidence]
        public DemoInfo Get()
        {
            using (var session = store.OpenSession()) {
                return session.Query<DemoInfo>().FirstOrDefault() ?? new DemoInfo();
            }
        }

        // POST api/demo
        [AuthorizeWithResidence]
        public DemoInfo Post([FromBody]int? tapTime)
        {
            return store.UpdateDemoInfo(info => {
                info.Residences = residenceStore.Count;
                info.Taps++;
            });
        }

        IResidenceStore IResidenceStoreProvider.ResidenceStore { get { return residenceStore; } }
    }

}
