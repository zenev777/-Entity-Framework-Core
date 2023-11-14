using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        [Required]
        [MaxLength(Constraints.InitialsLenght)]
        public string Initials { get; set; }

        public decimal Budget { get; set; }

        

    }
}