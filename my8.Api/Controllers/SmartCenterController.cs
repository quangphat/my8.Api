using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using my8.Api.IBusiness;
using my8.Api.Infrastructures;
using my8.Api.Models;
using my8.Api.SmartCenter;

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class SmartCenterController : BaseController
    {
        ISmartCenter m_SmartCenter;
        NotificationHub m_NotificationHub;
        private readonly IHubContext<NotificationHub> _hubContext;
        public SmartCenterController(CurrentProcess process, ISmartCenter smartCenter,NotificationHub notificationHub, IHubContext<NotificationHub> hubContext):base(process)
        {
            m_SmartCenter = smartCenter;
            m_NotificationHub = notificationHub;
            _hubContext = hubContext;
        }
		[HttpPost]
        [Route("api/smartcenter/create")]
        public async Task<IActionResult> Create([FromBody] Person model)
        {
            //model = new JobPost();

            //bool result = await m_SmartCenter.BroadcastToPerson(model);

            if(true)
            {
                //m_NotificationHub = new NotificationHub(_hubContext);
                //m_NotificationHub.AddUser(model);
                await m_NotificationHub.SendToPeople(model);
               //await  _hubContext.Clients.All.InvokeAsync("sendNotification", model);
            }

            return Json(true);
        }
    }
}