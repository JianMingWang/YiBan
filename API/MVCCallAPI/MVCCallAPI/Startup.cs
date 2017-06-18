using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCCallAPI.Startup))]
namespace MVCCallAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
