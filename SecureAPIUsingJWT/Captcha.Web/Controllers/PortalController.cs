using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Captcha.Web.Models;
using DNTCaptcha.Core;
using DNTCaptcha.Core.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Captcha.Web.Controllers
{
    public class PortalController : Controller
    {
        private readonly IDNTCaptchaValidatorService _validatorService;

        public PortalController(IDNTCaptchaValidatorService validatorService)
        {
            _validatorService = validatorService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateDNTCaptcha(
            ErrorMessage = "Please Enter Valid Captcha",
            CaptchaGeneratorLanguage = Language.English,
            CaptchaGeneratorDisplayMode = DisplayMode.ShowDigits)]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.ShowDigits))
                {
                    ModelState.AddModelError(DNTCaptchaTagHelper.CaptchaInputName, "Please Enter Valid Captcha.");
                    return View("Login");
                }

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
