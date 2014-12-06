using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LaCeibaNetv4.Startup))]
namespace LaCeibaNetv4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();

        }
    }
}
