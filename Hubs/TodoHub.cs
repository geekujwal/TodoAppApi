using Microsoft.AspNetCore.SignalR;

namespace TodoAppApi.Hubs
{
    public class TodoListHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("connected");
        }
        public async Task SendMessage(string message)
        {
            Console.WriteLine("SendMessage method called"); // Add this
            Console.WriteLine(message);
            await Clients.All.SendAsync("ReceiveMessage", message + "hello world");
        }

        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}