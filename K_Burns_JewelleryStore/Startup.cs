using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(K_Burns_JewelleryStore.Startup))]
namespace K_Burns_JewelleryStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
