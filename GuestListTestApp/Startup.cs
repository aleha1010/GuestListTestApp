using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuestListTestApp.Startup))]
namespace GuestListTestApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
