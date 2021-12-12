using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppAspNetMvcIdentity.Startup))]
namespace WebAppAspNetMvcIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
