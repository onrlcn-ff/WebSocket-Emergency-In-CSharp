using AcilYardimVeritabani.Models;
using Microsoft.Extensions.DependencyInjection;
using OperatorUygulamasi;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

class Program
{
    static async Task Main(string[] args)
    {
        var webSocketClient = new WebSocketClient();
        await webSocketClient.ConnectAndReceiveMessageAsync(@"ws://localhost:5000/ws");

        //Console.WriteLine("Ekip Adını Girin:");
        //string ekipAdi = Console.ReadLine();
        //Ekip ekip = new Ekip(ekipAdi);
        //ServiceCollection services = new ServiceCollection();
        //Startup.ConfigureServices(services);
        //ServiceProvider serviceProvider = services.BuildServiceProvider();

        //using (var scope = serviceProvider.CreateScope())
        //{
        //    //AcilYardimContext dbContext = scope.ServiceProvider.GetRequiredService<AcilYardimContext>();

        //    using (var dbContext = scope.ServiceProvider.GetRequiredService<AcilYardimContext>())
        //    {
        //        var acilDurum = new AcilDurum { Ekip = "OperatorAdi", Operator = ekip.Ad, Mesaj = gelenMesaj, TarihSaat = DateTime.Now };
        //        dbContext.AcilDurumlar.Add(acilDurum);
        //        await dbContext.SaveChangesAsync();
        //    }
        //}

    }

}