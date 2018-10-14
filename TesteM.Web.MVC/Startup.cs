using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TesteM.Web.MVC.Startup))]
namespace TesteM.Web.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
