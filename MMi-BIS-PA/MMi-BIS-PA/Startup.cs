using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MMi_BIS_PA.Startup))]
namespace MMi_BIS_PA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
