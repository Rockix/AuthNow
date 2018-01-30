using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AuthNow.Startup))]
namespace AuthNow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
