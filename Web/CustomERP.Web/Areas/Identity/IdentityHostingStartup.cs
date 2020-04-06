using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CustomERP.Web.Areas.Identity.IdentityHostingStartup))]

namespace CustomERP.Web.Areas.Identity
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
