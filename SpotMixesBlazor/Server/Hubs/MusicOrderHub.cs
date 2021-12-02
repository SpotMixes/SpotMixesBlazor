using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SpotMixesBlazor.Server.Hubs
{
    public class MusicOrderHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}