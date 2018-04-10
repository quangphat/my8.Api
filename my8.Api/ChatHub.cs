﻿using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace my8.Api
{
    public class ChatHub : Hub
    {
        public void SendToAll(string name, string message)
        {
            Clients.All.InvokeAsync("sendToAll", name, message);
            string id = Context.ConnectionId;
            string names = Context.User.Identity.Name;
            //Clients.Group(names).InvokeAsync("dd",names, message);
        }
        public List<string> GetlistString()
        {
            return new List<string>();
        }
    }
}
