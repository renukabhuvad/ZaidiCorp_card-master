using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZaidiCorp.Startup))]
namespace ZaidiCorp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
