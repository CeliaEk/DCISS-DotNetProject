using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DotNetSportAsso.Startup))]
namespace DotNetSportAsso
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
