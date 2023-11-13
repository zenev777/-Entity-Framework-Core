using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models
{
    public class Bet
    {
        public int BetId { get; set; }

        public int Amount { get; set; }

        public string Prediction { get; set; }

        public DateTime DateTime { get; set; }

        //relation not ready
        public int GameId { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

    }
}