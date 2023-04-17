using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class WebSocketClient
{
    public async Task ConnectAndReceiveMessageAsync(string serverUrl)
    {
        while (true) // Sunucu bağlantısı sürekli yeniden denenir
        {
            using (var clientWebSocket = new ClientWebSocket())
            {
                try
                {
                    await clientWebSocket.ConnectAsync(new Uri(serverUrl), CancellationToken.None);
                }
                catch (WebSocketException)
                {
                    Console.WriteLine("WebSocket sunucusuna bağlanılamadı. Birazdan yeniden denenecek.");
                    await Task.Delay(TimeSpan.FromSeconds(5)); // Belirli bir süre bekleyin ve yeniden deneyin
                    continue;
                }

                Console.WriteLine("Connected to the WebSocket server.");
                Console.WriteLine("Adınız : ");
                string name = Console.ReadLine();
                while (clientWebSocket.State == WebSocketState.Open)
                {

                    Console.WriteLine("Görev Bekleniyor...");
                    string receivedMessage = await MessageManager.ReceiveMessageAsync(clientWebSocket);
                    Console.WriteLine("Received message: " + receivedMessage);

                    Console.WriteLine("Görevi Onayla");
                    Console.WriteLine("Onaylmak için - 1");
                    Console.WriteLine("Reddetmek için - 2");
                    string option = Console.ReadLine();
                    string message = "";
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    switch (option)
                    {
                        case "1":
                            message = "Onaylandı";
                            break;
                        case "2":
                            message = "Reddedildi";
                            break;
                    }
                    await MessageManager.SendMessageAsync(clientWebSocket, $"{name}: {message}");
                    await Task.Delay(TimeSpan.FromSeconds(2));
                }

                // WebSocket bağlantısını kapatma
                await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }

            Console.WriteLine("Bağlantı kesildi. Birazdan yeniden bağlanmayı deneyecek.");
            await Task.Delay(TimeSpan.FromSeconds(5)); // Belirli bir süre bekleyin ve yeniden deneyin
        }
    }

}
public class MessageManager
{
    public static async Task SendMessageAsync(WebSocket webSocket, string message)
    {
        try
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            var messageSegment = new ArraySegment<byte>(messageBytes);
            await webSocket.SendAsync(messageSegment, WebSocketMessageType.Text, true, CancellationToken.None);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Mesaj gönderilirken hata oluştu: {ex.Message}");
        }
    }

    public static async Task<string> ReceiveMessageAsync(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
        return receivedMessage;
    }
}
