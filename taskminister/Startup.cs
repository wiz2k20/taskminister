using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(taskminister.Startup))]

namespace taskminister
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            //app.MapSignalR(new HubConfiguration() { EnableJSONP = true });
        }
    }

}
