using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyFitPlan_WebAdmin.Startup))]
namespace MyFitPlan_WebAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
