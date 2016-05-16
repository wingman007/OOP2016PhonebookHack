using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Phonebook.Startup))]
namespace Phonebook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
