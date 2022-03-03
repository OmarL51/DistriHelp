
using DistriHelp.API.Data.Entities;
using DistriHelp.API.Helpers;
using DistriHelp.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistriHelp.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IEmailSender _emailSender;

        public AccountController(IUserHelper userHelper, IMailHelper mailHelper, IConverterHelper converterHelper, ICombosHelper combosHelper, IEmailSender emailSender)
        {
            _userHelper = userHelper;
            _mailHelper = mailHelper;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
            _emailSender = emailSender;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos.");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "El correo ingresado no corresponde a ningún usuario.");
                    return View(model);
                }




                string myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
                string link = Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);
                var message = new Message(new string[] { model.Email }, "DistriHelp - Reseteo de contraseña", $"<h1>DistriHelp - Reseteo de contraseña</h1>" +
                   $"Para establecer una nueva contraseña haga clic en el siguiente enlace:</br></br>" +
                   $"<a href = \"{link}\">Cambio de Contraseña</a>", null, "soporte@distrimedical.com.co", "Distrimedical1%");
                _emailSender.SendEmailAsync(message);
                ViewBag.Message = "Las instrucciones para el cambio de contraseña han sido enviadas a su email.";
                return View();

            }

              
                return View(model);
        }

        public IActionResult ResetPassword(string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
           
            User user = await _userHelper.GetUserAsync(model.UserName);
            if (user != null)
            {
                IdentityResult result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {

                   
                    await _userHelper.UpdateUserPassAsync(model);

                    ViewBag.Message = "Contaseña cambiada.";
                    return View();
                }
               
         

                ViewBag.Message = "Error cambiando la contraseña.";
                return View(model);
            }

            ViewBag.Message = "Usuario no encontrado.";
            return View(model);
        }

    }
}