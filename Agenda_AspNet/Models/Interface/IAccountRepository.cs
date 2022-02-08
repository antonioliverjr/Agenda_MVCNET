using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Agenda_AspNet.Models.Interface
{
    public interface IAccountRepository
    {
        Task<IdentityUser> ObterUser(string username, string password);
    }
}
