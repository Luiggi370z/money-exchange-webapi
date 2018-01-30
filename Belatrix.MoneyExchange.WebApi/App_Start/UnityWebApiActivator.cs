using System.Web.Http;

using Unity.AspNet.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Belatrix.MoneyExchange.WebApi.UnityWebApiActivator), nameof(Belatrix.MoneyExchange.WebApi.UnityWebApiActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Belatrix.MoneyExchange.WebApi.UnityWebApiActivator), nameof(Belatrix.MoneyExchange.WebApi.UnityWebApiActivator.Shutdown))]

namespace Belatrix.MoneyExchange.WebApi
{
    /// <summary>
    /// Provides the bootstrapping for integrating Unity with WebApi when it is hosted in ASP.NET.
    /// </summary>
    public static class UnityWebApiActivator
    {
        public static void Start()
        {
            var resolver = new UnityHierarchicalDependencyResolver(UnityConfig.Container);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        public static void Shutdown()
        {
            UnityConfig.Container.Dispose();
        }
    }
}