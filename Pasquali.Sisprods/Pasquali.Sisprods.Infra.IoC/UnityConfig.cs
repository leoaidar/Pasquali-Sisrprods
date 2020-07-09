using Pasquali.Sisprods.Domain.Handlers;
using Pasquali.Sisprods.Domain.Repositories;
using Pasquali.Sisprods.Infra.Data.Contexts;
using Pasquali.Sisprods.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Pasquali.Sisprods.Infra.IoC
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            //Handlers            
            container.RegisterType<ClientHandler, ClientHandler>();
            container.RegisterType<ProductHandler, ProductHandler>();

            //Data
            container.RegisterType<IClientRepository, ClientRepository>();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<SisprodsContext, SisprodsContext>();


            //container.Resolve<T>();
            //container.RegisterType<IUnitOfWork, UnitOfWork>(new PerRequestLifetimeManager());
            //container.RegisterType<IMyService, MyService>("MyService");
            //container.RegisterType<IMyService, MyService>(new InjectionConstructor(
            //    new ResolvedParameter<IServiceClient>()
            //));

            //GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }


    }
}
