﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Belatrix.MoneyExchange.WebApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }
    }
}
