using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InfrustructureMVC.Startup))]
namespace InfrustructureMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
