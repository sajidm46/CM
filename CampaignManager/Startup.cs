using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CampaignManager.Startup))]
namespace CampaignManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
