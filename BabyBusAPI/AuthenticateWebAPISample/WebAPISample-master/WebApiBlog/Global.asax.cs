﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApiBlog.App_Start;
using WebApiBlog.Core;
using WebApiBlog.Core.DataAccess;
using WebApiBlog.Core.DependencyResolvers;
using WebApiBlog.Core.Handlers;
using WebApiBlog.Core.Installers;
using WebApiBlog.Core.MediaFormatters;

namespace WebApiBlog
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : ContainerApplication
    {
        protected override void AppStart()
        {
            WireUpDependencyResolvers();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.Add(new QrMediaFormatter());
            GlobalConfiguration.Configuration.MessageHandlers.Add(
                new AuthenticationHandler(Container.Resolve<IAccessTokenRepository>(),
                                          Container.Resolve<IUserRepository>()));
            GlobalConfiguration.Configuration.MessageHandlers.Add(new ResponseHeaderHandler());
        }

        private void WireUpDependencyResolvers()
        {
            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(Container);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(Container));
        }
    }
}