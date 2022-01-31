using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_AspNet.Models.Interface
{
    public interface IAccount
    {
        Task<bool> AuthUser(string email, string password, bool remember);
        Task<bool> RegisterUser(string nome, string email, string password);
        Task Logout();
    }
}
