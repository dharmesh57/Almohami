using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AlmohamiWeb.Startup))]
namespace AlmohamiWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //
        }
    }
}
