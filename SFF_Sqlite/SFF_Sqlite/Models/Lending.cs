using System;

namespace SFF_Sqlite.Models
{
    public class Lending
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime LendingDate { get; set; } = DateTime.Now;
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int FilmClubId { get; set; }
        public FilmClub FilmClub { get; set; }
        public Review Review { get; set; }

    }
}
