using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GeekBooks.Startup))]
namespace GeekBooks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
