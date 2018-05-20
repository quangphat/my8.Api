
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace my8.Api
{
    public class ChatHub : Hub
    {
        public void SendToAll(string name, string message)
        {
            Clients.All.SendAsync("sendToAll", name, message);
            string id = Context.ConnectionId;
            string names = Context.User.Identity.Name;
            //Clients.Group(names).InvokeAsync("dd",names, message);
        }
        public override async Task OnConnectedAsync()
        {
            
            await Groups.AddAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
            string id = Context.ConnectionId;
            string value = Context.GetHttpContext().Request.Cookies[""];
        }
    }
}
