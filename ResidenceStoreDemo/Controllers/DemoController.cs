using System.Collections.Generic;
using System.Web.Http;

namespace ResidenceStoreDemo.Controllers
{
    using System.Linq;
    using Raven.Client;
    using ResidenceStore.RavenDB;

    public class DemoController : ApiController
    {
        private readonly IDocumentStore store;
        private RavenResidenceStore residenceStore;

        public DemoController()
        {
            store = MvcApplication.DocumentStore;
            residenceStore = MvcApplication.ResidenceStore;
        }

        // GET api/demo
        public DemoInfo Get()
        {
            using (var session = store.OpenSession()) {
                return session.Query<DemoInfo>().FirstOrDefault() ?? new DemoInfo();
            }
        }

        // POST api/demo
        public void Post([FromBody]int tapTime)
        {
            store.UpdateDemoInfo(info => {
                info.Residences = residenceStore.Count;
                info.Taps++;
            });
        }

    }

    public class DemoInfo
    {
        public int Residences { get; set; }
        public int Registrations { get; set; }
        public int Verifications { get; set; }
        public int Reauthorizations { get; set; }
        public int Removals { get; set; }
        public int Taps { get; set; }
    }
}
