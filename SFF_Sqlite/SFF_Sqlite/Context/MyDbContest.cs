using SFF_Sqlite.Models;
using Microsoft.EntityFrameworkCore;

namespace SFF_Sqlite.Context
{
    public class MyDbContest : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Lending> Lendings { get; set; }
        public DbSet<FilmClub> FilmClubs { get; set; }

        public MyDbContest(DbContextOptions<MyDbContest> options) : base(options)
        {
        }

    }
    
}


