using Microsoft.AspNetCore.SignalR;
using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api
{
    public class NotificationHub:Hub
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        List<UserConnection> uList = new List<UserConnection>();
        public NotificationHub(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
            m_Usr = new UserConnection();
        }
        public async Task SendToPeople(Person person)
        {
            UserConnection usr = uList.FirstOrDefault(p => p.UserId == person.PersonId);
            await _hubContext.Clients.All.SendAsync("sendNotification", person.DisplayName);
            //Clients.All.InvokeAsync("sendNotification", people[0].DisplayName);
            string id = Context.ConnectionId;
            string names = Context.User.Identity.Name;
            //Clients.Group(names).InvokeAsync("dd",names, message);
        }
        UserConnection m_Usr;
        public override Task OnConnectedAsync()
        {
            m_Usr = new UserConnection();
            AddConnectionId(Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public void AddConnectionId(string connectionId)
        {

            m_Usr.ConnectionID = connectionId;
        }
        public void AddUser(Person person)
        {
            m_Usr.UserId = person.PersonId;
            uList.Add(m_Usr);
        }
    }
    public class UserConnection
    {
        public string UserId { set; get; }
        public string ConnectionID { set; get; }
    }
}
