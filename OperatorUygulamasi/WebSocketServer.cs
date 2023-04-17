using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;
using System.IO.Pipelines;

public class WebSocketMiddleware
{
    private readonly RequestDelegate _next;

    public WebSocketMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path == "/ws")
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await HandleWebSocketCommunication(context, webSocket);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }
        else
        {
            await _next(context);
        }
    }

    private async Task HandleWebSocketCommunication(HttpContext context, WebSocket webSocket)
    {
        Console.WriteLine(" New WebSocket Client Connected.");
        string message = "";
        MessageManager messageManager = new MessageManager();

        //await messageManager.HandleMessageManager(context, webSocket, message);
        Console.WriteLine("Adınız : ");
        string name = Console.ReadLine(); // ReadLine() kullanarak string okuyun

        while (true)
        {
            Console.WriteLine("Göndermek istediğiniz mesajı seçin:");
            Console.WriteLine("1. Yangın");
            Console.WriteLine("2. Ambulans");
            Console.WriteLine("3. Polis");
            Console.WriteLine("4. Çıkış");

            string option;
            option = Console.ReadLine();

            if (option == "4")
            {
                await MessageManager.SendMessageAsync(webSocket, $"{name} çıkış yaptı.");
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Çıkış", CancellationToken.None);
                break;
            }
            else
            {

                switch (option)
                {
                    case "1":
                        message = "Yangın";
                        break;
                    case "2":
                        message = "Ambulans";
                        break;
                    case "3":
                        message = "Polis";
                        break;
                }

                if (!string.IsNullOrEmpty(message))
                {
                    await MessageManager.SendMessageAsync(webSocket, message);
                }
            }
            Console.WriteLine("Cevap Bekleniyor...");
            string result;
            result = await MessageManager.ReceiveMessageAsync(webSocket);
            Console.WriteLine(result);
            message = "";
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
}