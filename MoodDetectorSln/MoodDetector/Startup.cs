using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoodDetector.Startup))]
namespace MoodDetector
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
