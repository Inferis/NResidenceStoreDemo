namespace ResidenceStoreDemo.Controllers
{
    using Raven.Client;
    using ResidenceStore;
    using ResidenceStore.RavenDB;
    using ResidenceStore.Web.Mvc.Controllers;

    public class ResidenceController : ResidenceVerifierController
    {
        private readonly IDocumentStore store;

        /// <summary>
        /// Example residence store controller. You should probably change the used residence store implementation
        /// to fit your own needs.
        /// </summary>
        public ResidenceController()
            : base(new RavenResidenceStore(MvcApplication.DocumentStore, new ResidenceStoreMailer()))
        {
            store = MvcApplication.DocumentStore;
        }

        protected override void OnResidenceRegistered(ResidenceInfo residence)
        {
            store.UpdateDemoInfo(info => {
                var mgr = this.ResidenceStore as IResidenceStoreManager;
                if (mgr != null)
                    info.Residences = mgr.Count;
                info.Registrations++;
            });
        }

        protected override void OnResidenceConfirmed(ResidenceInfo residence)
        {
            store.UpdateDemoInfo(info => {
                info.Verifications++;
            });
        }

        protected override void OnResidenceReauthorized(ResidenceInfo residence)
        {
            store.UpdateDemoInfo(info => {
                info.Reauthorizations++;
            });
        }

        protected override void OnResidenceDeleted(ResidenceInfo residence)
        {
            store.UpdateDemoInfo(info => {
                var mgr = this.ResidenceStore as IResidenceStoreManager;
                if (mgr != null)
                    info.Residences = mgr.Count;
                info.Removals++;
            });
        }
    }
}