using BetsAppMVC.Models.Api;
using BetsAppMVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BetsAppMVC.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BetsController : ControllerBase
    {
        private IEventsService _eventsService;
        private IPlayersService _playersService;
        private IBetsService _betsService;
        private IHttpContextAccessor _httpContextAccessor;
        public BetsController(IEventsService eventsService, IPlayersService playersService, IBetsService betsService, IHttpContextAccessor httpContextAccessor) 
        {
            _eventsService = eventsService;
            _playersService = playersService;
            _betsService = betsService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            return new JsonResult(_betsService.GetAll());
        }

        [HttpPost]
        public JsonResult MakeBet(BetsApiModel model)
        {
            var playerId = _httpContextAccessor.HttpContext.Session.GetInt32("PlayerId");
            var player = _playersService.GetPlayerById(playerId.GetValueOrDefault());

            if (player == null)
            {
                return new JsonResult("Player not found");
            }

            var sportEvent = _eventsService.GetEventById(model.EventId);
            if (sportEvent == null)
            {
                return new JsonResult("Sport event not found");
            }

            if (model.Odd <= 0)
            {
                return new JsonResult("Odd can't be less than 0");
            }

            if (model.Money <= 0)
            {
                return new JsonResult("Amount of money should be positive");
            }

            if (player.Balance < model.Money)
            {
                return new JsonResult("Balance should be bigger than the bet");
            }

            foreach (var actualOdd in sportEvent.Odds)
            {
                var diff = Math.Abs(model.Odd - actualOdd.Value);
                if (!(diff / actualOdd.Value > 0.05 && sportEvent.IsLive) && !(diff / actualOdd.Value > 0.10 && sportEvent.IsLive))
                {
                    _playersService.MakeBet(playerId.GetValueOrDefault(), model.Odd, model.Money);
                    _betsService.MakeBet(playerId.GetValueOrDefault(), model.EventId, model.Odd, model.Money);
                    return new JsonResult("Success! Wish you good luck");
                }
            }

            return new JsonResult("Tolerance is wrong, should be 10% for live events and 5% for pre-events");
        }
    }
}
