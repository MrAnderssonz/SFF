using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFF_Sqlite.Models;

namespace SFF_Sqlite.Repository
{
    public interface ILabelRepository
    {
        Task<ActionResult<Label>> GetLabel(int id);
    }
}
