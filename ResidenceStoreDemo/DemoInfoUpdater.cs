using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResidenceStoreDemo
{
    using Controllers;
    using Raven.Client;

    public static class DemoInfoUpdater
    {
        public static DemoInfo UpdateDemoInfo(this IDocumentStore store, Action<DemoInfo> action)
        {
            using (var session = store.OpenSession()) {
                var info = session.Query<DemoInfo>().FirstOrDefault() ?? new DemoInfo();
                action(info);
                session.Store(info);
                session.SaveChanges();
                return info;
            }
        }
    }
}