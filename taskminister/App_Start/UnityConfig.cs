using System.Web.Mvc;
using Unity;
using Unity.Mvc5;


using taskminister.musik.Interface;
using taskminister.musik.Repository;
using taskminister.musik.Service;

using taskminister.security.Database;


namespace taskminister
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            // e.g. container.RegisterType<ITestService, TestService>();
			var container = new UnityContainer();

            // taskminister.musik
            container.RegisterType<IRepoMusik, RepoMusik>();
            container.RegisterType<IServMusik, ServMusik>();

            // taskminister.security
            container.RegisterType<IConnection, Connection>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}