using OperatorUygulamasi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.IO.Pipelines;
using System.Net.WebSockets;

class Program
{
    static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        await host.RunAsync();

        

        }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
     Host.CreateDefaultBuilder(args)
         .ConfigureLogging(logging =>
         {
             logging.ClearProviders(); // Varsayılan loglama sağlayıcılarını temizle
             logging.AddConsole(); // Konsol loglama sağlayıcısını ekle
             logging.SetMinimumLevel(LogLevel.Warning); // Loglama düzeyini Warning olarak ayarla
         })
         .ConfigureWebHostDefaults(webBuilder =>
         {
             webBuilder.UseUrls("http://localhost:5000");
             webBuilder.ConfigureServices(services => { });
             webBuilder.Configure(app =>
             {
                 app.UseRouting();
                 app.UseWebSockets();
                 app.UseMiddleware<WebSocketMiddleware>();
             });
         });

    //ServiceCollection services = new ServiceCollection();
    //Startup.ConfigureServices(services);
    //ServiceProvider serviceProvider = services.BuildServiceProvider();

    //using (var scope = serviceProvider.CreateScope())
    //{
    //    //AcilYardimContext dbContext = scope.ServiceProvider.GetRequiredService<AcilYardimContext>();

    //    using (var dbContext = scope.ServiceProvider.GetRequiredService<AcilYardimContext>())
    //    {
    //        var acilDurum = new AcilDurum { Operator = operator_.Ad, Ekip = ekipAdi, Mesaj = mesaj, TarihSaat = DateTime.Now };
    //        dbContext.AcilDurumlar.Add(acilDurum);
    //        await dbContext.SaveChangesAsync();
    //    }
    //}

    //while (true)
    //{
    //Console.Write("Mesaj Gönder");
    //string mesaj = Console.ReadLine().ToString();
    //webSocketServer.SendMessage(mesaj);
    //Console.ReadLine();

    //}
}



