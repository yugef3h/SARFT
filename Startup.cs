using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Upload_Photo.Startup))]
namespace Upload_Photo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
