using BetsAppMVC.Models;

namespace BetsAppMVC.Services
{
    public interface IPlayersService
    {
        List<Player> GetPlayers();
        Player GetPlayerById(int id);
        Player GetPlayerByUserNameAndPassword(string userName, string password);
        void MakeBet(int playerId, double odd, double money);
    }
    public class PlayersService : IPlayersService
    {
        private List<Player> _players;
        private string fileName = "players.json";
        public PlayersService()
        {
            _players = JsonHelper.ReadJson<Player>(fileName);
        }
        public List<Player> GetPlayers()
        {
            return _players;
        }

        public Player GetPlayerById(int id)
        {
            return _players.FirstOrDefault(it => it.Id == id);
        }

        public Player GetPlayerByUserNameAndPassword(string userName, string password)
        {
            return _players.FirstOrDefault(it => it.UserName == userName && it.Password == password);
        }

        public void MakeBet(int playerId, double odd, double money)
        {
            var player = GetPlayerById(playerId);
            if (player != null)
            {
                if (player.Balance > money)
                {
                    player.Balance -= money;
                }
            }
        }
    }
}
