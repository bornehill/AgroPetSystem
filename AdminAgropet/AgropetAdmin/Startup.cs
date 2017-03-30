using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AgropetAdmin.Startup))]
namespace AgropetAdmin
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
