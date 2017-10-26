using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iAssetTechnicalTest.Startup))]
namespace iAssetTechnicalTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
