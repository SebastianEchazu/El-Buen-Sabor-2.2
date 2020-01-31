using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(EmpleadosEBS.Areas.Identity.IdentityHostingStartup))]
namespace EmpleadosEBS.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}