using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SpotMixesBlazor.Server.Hubs
{
    public class ReactionHub : Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("ReceiveMessage");
        }
    }
}