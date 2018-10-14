using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using SimpleInjector.Integration.Web.Mvc;
using TesteM.Web.MVC.AutoMapper;
using TestM.Infra.CrossCutting;
namespace TesteM.Web.MVC
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperWebProfile>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(DiContainer.RegisterDependencies()));
            // DataTables.AspNet registration with default options.
            //DataTables.AspNet.Mvc5.Configuration.RegisterDataTables();
        }
    }
}