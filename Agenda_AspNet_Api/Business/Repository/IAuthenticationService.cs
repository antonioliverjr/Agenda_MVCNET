using Agenda_AspNet_Api.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_AspNet_Api.Business.Repository
{
    public interface IAuthenticationService
    {
        string GerarToken(User user);
    }
}
