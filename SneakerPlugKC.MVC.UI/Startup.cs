using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SneakerPlugKC.MVC.UI.Startup))]
namespace SneakerPlugKC.MVC.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
