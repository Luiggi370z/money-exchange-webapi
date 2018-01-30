using Belatrix.MoneyExchange.Data;
using Belatrix.MoneyExchange.Server;
using Belatrix.MoneyExchange.Server.Queries;
using System;
using Unity;
using Unity.Lifetime;

namespace Belatrix.MoneyExchange.WebApi
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> _container = Configure(() => new PerResolveLifetimeManager());

        public static IUnityContainer Container => _container.Value;


        public static IUnityContainer RegisterDependencies(Func<LifetimeManager> getLifetimeManager)
        {
            var container = new UnityContainer();

            container
                .RegisterType<IUnitOfWork, MoneyExchangeContext>(getLifetimeManager())
                .RegisterType<RatesQuery>(new ContainerControlledLifetimeManager())
                .RegisterType<IRateRepository, RateRepository>(
                    getLifetimeManager());

            return container;
        }

        private static Lazy<IUnityContainer> Configure(Func<LifetimeManager> getLifetimeManager) =>
            new Lazy<IUnityContainer>(() => RegisterDependencies(getLifetimeManager));
    }
}