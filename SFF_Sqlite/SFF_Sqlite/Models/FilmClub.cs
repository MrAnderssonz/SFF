using System.Collections.Generic;

namespace SFF_Sqlite.Models
{
    public class FilmClub
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Lending> Lendings { get; set; } = new List<Lending>();
    }
}
