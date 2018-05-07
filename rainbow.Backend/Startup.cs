using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(rainbow.Backend.Startup))]
namespace rainbow.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
