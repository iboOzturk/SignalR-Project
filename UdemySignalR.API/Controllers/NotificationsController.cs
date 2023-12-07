using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using UdemySignalR.API.Hubs;

namespace UdemySignalR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IHubContext<MyHub> _hubContext;

        public NotificationsController(IHubContext<MyHub> hubContext)
        {
            _hubContext = hubContext;
        }
        [HttpGet("{teamCount}")]
        public async Task<IActionResult> SetTeamCount(int teamCount)
        {
            MyHub.TeamCount = teamCount;
            await _hubContext.Clients.All.
                SendAsync("Notify", $"Arkadaşlar takımlar {teamCount} kişi olacak");
            return Ok();

        }
    }
}
