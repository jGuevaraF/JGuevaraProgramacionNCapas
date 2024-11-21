using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PLIdentity.Startup))]
namespace PLIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
