using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFF_Sqlite.Context;
using SFF_Sqlite.Models;

namespace SFF_Sqlite.Repository
{
    public class LabelRepository : ILabelRepository
    {
        private readonly MyDbContest _context;

        public LabelRepository(MyDbContest context)
        {
            this._context = context;
        }

        // Method to make a label for Posten to print
        public async Task<ActionResult<Label>> GetLabel(int id)
        {

            var rent = await _context.Lendings
                                     .Where(r => r.Id == id)
                                     .Include(m => m.Movie)
                                     .Include(f => f.FilmClub)
                                     .FirstOrDefaultAsync();

            var label = new Label
            {
                Title = rent.Movie.Title,
                City = rent.FilmClub.City,
                LendingDate = rent.LendingDate
            };

            return label;
        }
    }
}
