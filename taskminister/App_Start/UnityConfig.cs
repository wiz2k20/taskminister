using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using taskminister.musik.Interface;
using taskminister.musik.Repository;
using taskminister.musik.Service;
using taskminister.security.Database;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System;
using IConnection = taskminister.security.Database.IConnection;
using taskminister.Hubs;
using Unity.Injection;

namespace taskminister
{
    public static class UnityConfig
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();
            var unityDependencyResolver = new UnityDependencyResolver(container);

            DependencyResolver.SetResolver(unityDependencyResolver);
            GlobalHost.DependencyResolver = new SignalRUnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // taskminister.musik
            container.RegisterType<IRepoMusik, RepoMusik>();
            container.RegisterType<IServMusik, ServMusik>();

            // taskminister.security
            container.RegisterType<IConnection, Connection>();
            //container.RegisterType<ISomeInterface, SomeInterface>();

            // hub
            container.RegisterFactory<HubMusik>(CreateMyHub);

            return container;
        }

        public class SignalRUnityDependencyResolver : DefaultDependencyResolver
        {
            private IUnityContainer _container;

            public SignalRUnityDependencyResolver(IUnityContainer container) {
                _container = container;
            }

            public override object GetService(Type serviceType) {
                if (_container.IsRegistered(serviceType)) return _container.Resolve(serviceType);
                else return base.GetService(serviceType);
            }

            public override IEnumerable<object> GetServices(Type serviceType) {
                if (_container.IsRegistered(serviceType)) return _container.ResolveAll(serviceType);
                else return base.GetServices(serviceType);
            }
        }

        private static object CreateMyHub(IUnityContainer p)
        {
            var myHub = new HubMusik(p.Resolve<IServMusik>());

            return myHub;
        }

        //public static void RegisterComponents()
        //{
        //    var container = new UnityContainer();

        //    // taskminister.musik
        //    container.RegisterType<IRepoMusik, RepoMusik>();
        //    container.RegisterType<IServMusik, ServMusik>();

        //    // taskminister.security
        //    container.RegisterType<IConnection, Connection>();

        //    DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        //}

    }
}