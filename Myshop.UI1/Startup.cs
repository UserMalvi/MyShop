using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Myshop.UI1.Startup))]
namespace Myshop.UI1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
