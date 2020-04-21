using System;
using System.ComponentModel.DataAnnotations;

namespace SFF_Sqlite.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Range(1, 5)]
        public int Grade { get; set; }
        public string Trivia { get; set; } 
        public int LendingId { get; set; }
        public Lending Lending { get; set; }

    }
}
