using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common.Logging;
using System.Threading.Tasks;
using log4net.Config;
using Quartz;
using Quartz.Impl;
using ScrapeRateAPI.BLL;


namespace ScrapeRateAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(WebApiApplication));
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            XmlConfigurator.ConfigureAndWatch(new FileInfo(Server.MapPath("~/log4net.config")));
            _log.Info("Service turned on.");
            //啟動quartz 排程
            IScheduler scheduler = new StdSchedulerFactory().GetScheduler();
            scheduler.Start();
        }
    }
}
