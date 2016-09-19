using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(backupmyddb.Startup))]
namespace backupmyddb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
