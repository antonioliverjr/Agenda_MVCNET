using Agenda_AspNet.Models.Interface;
using Agenda_AspNet.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_AspNet.Controllers
{
    public class AccountController : Controller
    {
        public readonly IAccount _account;

        public AccountController(IAccount account)
        {
            _account = account;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModelView login, string returnUrl=null)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            if (await _account.AuthUser(login.UserName, login.Password, login.RememberMe))
            {
                return RedirectToRoute(new { controller = "Contato", action = "Index"});
            }

            TempData["error"] = "Usuário e Senha não conferem ou não constam cadastro!";
            return View(login);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (await _account.RegisterUser(user.UserName, user.Email, user.Password))
            {
                return RedirectToRoute(new { controller = "Contato", action = "Index" });
            }
            TempData["error"] = "Erro ao registrar o usuário, tente novamente.";
            return View(user);
        }

        public IActionResult ForgetPass()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            _account.Logout();
            return Redirect(nameof(Login));
        }
    }
}
