using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFF_Sqlite.Models;
using SFF_Sqlite.DTO;

namespace SFF_Sqlite.Repository
{
    public interface ILendingRepository
    {
        Task<ActionResult<IEnumerable<LendingDto>>> GetAllLendings();
        Task<ActionResult<LendingDto>> GetLending(int id);
        Task<ActionResult<LendingDto>> LendMovie(Lending lending);
        Task<ActionResult<LendingDto>> ReturnMovie(int id);
    }
}
