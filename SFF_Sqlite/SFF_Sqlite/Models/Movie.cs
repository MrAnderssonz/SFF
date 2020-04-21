using System.Collections.Generic;

namespace SFF_Sqlite.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal AverageGrade { get; set; }
        public int MaxLending { get; set; } = 10;
        public ICollection<Lending> Lendings { get; set; } = new List<Lending>();
    }
}
