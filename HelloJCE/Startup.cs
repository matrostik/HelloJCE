using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HelloJCE.Startup))]
namespace HelloJCE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
