using Microsoft.EntityFrameworkCore;
using P02_FootballBetting.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        private const string conectionString = "Server=DESKTOP-GUVMUS8\\SQLEXPRESS;Database=MinionsDB; Integrated Security = true;";

        public DbSet<Town> Towns { get; set; }

        public DbSet<Country> Countires { get; set; }

        public DbSet<Color> Colors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conectionString); 
        }
    }
}
