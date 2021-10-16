using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobHunt.Startup))]
namespace JobHunt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
