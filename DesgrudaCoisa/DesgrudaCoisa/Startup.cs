using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DesgrudaCoisa.Startup))]
namespace DesgrudaCoisa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
