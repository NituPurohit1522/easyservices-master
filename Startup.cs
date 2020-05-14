using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EasyService.Startup))]
namespace EasyService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
