using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(log4net.Appender.SplunkAppenders.Demo.Startup))]
namespace log4net.Appender.SplunkAppenders.Demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
