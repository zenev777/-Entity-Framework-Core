﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models
{
    public class User
    {
        public int UserId { get; set; }


        [Required]
        [MaxLength(Constraints.UsernameLenght)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public string Email { get; set; }

        public decimal Balance { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Bet> Bets { get; set; }
    }
}
