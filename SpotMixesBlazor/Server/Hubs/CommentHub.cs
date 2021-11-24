using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SpotMixesBlazor.Server.Hubs
{
    public class CommentHub : Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("ReceiveMessage");
        }
    }
}