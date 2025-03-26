using BetsAppMVC.Models;

namespace BetsAppMVC.Services
{
    public interface IBetsService
    {
        void MakeBet(int playerId, int eventId, double odd, double money);
    }
    public class BetsService : IBetsService
    {
        private List<Bet> _bets;
        private string fileName = "bets.json";
        public BetsService()
        {
            _bets = JsonHelper.ReadJson<Bet>(fileName);
        }
        public void MakeBet(int playerId, int eventId, double odd, double money)
        {
            _bets.Add(new Bet() { PlayerId = playerId, EventId = eventId, Odd = odd, Money = money });
             JsonHelper.WriteJson<Bet>(fileName, _bets);
        }
    }
}
