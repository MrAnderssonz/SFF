using System;

namespace SFF_Sqlite.DTO
{
    public class LendingDto
    {
        public int Id { get; set; }
        public DateTime LendingDate { get; set; }
        public string Movie { get; set; }
        public string FilmStudio { get; set; }

    }
}