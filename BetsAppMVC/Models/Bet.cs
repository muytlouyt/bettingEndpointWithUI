using System.Security.Cryptography;

namespace BetsAppMVC.Models
{
    public class Bet
    {
        public int PlayerId { get; set; }
        public int EventId { get; set; }
        public double Odd { get; set; }
        public double Money { get; set; }
    }
}
