using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HotGuys.Startup))]
namespace HotGuys
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
