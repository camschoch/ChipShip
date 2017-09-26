using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChipShip.Startup))]
namespace ChipShip
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
