using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AcilYardimVeritabani.Models;

namespace OperatorUygulamasi
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("VeritabaniBaglanti");
            services.AddDbContext<AcilYardimContext>(options => options.UseSqlServer(connectionString));
        }
    }
}