using BetsAppMVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetsAppMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private IPlayersService _playersService;
        private IHttpContextAccessor _httpContextAccessor;
        public PlayersController(IPlayersService playersService, IHttpContextAccessor httpContextAccessor)
        { 
            _playersService = playersService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public JsonResult GetPlayer()
        {
            var playerId = _httpContextAccessor.HttpContext.Session.GetInt32("PlayerId");
            var player = _playersService.GetPlayerById(playerId.GetValueOrDefault());

            if (player == null)
            {
                return new JsonResult("Player not found");
            }

            return new JsonResult(new { balance = player.Balance, username = player.UserName });
        }
    }
}
