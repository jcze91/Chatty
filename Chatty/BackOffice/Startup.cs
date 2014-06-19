using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BackOffice.Startup))]
namespace BackOffice
{
    public class Startup
    {
        public static Configs.ContainerInjection container { get; set; }

        public void Configuration(IAppBuilder app)
        {
            container = new Configs.ContainerInjection();
            container.Configure();
            app.MapSignalR();
        }
    }
}
