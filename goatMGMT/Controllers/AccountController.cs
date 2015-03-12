using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using goatMGMT.Models;
using WebMatrix.WebData;
using System.Web.Security;

namespace goatMGMT.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginData)
        {
            if (ModelState.IsValid && WebSecurity.Login(loginData.Username, loginData.Password))
            {
                return RedirectToAction("Dashboard", "Home");
            }
            ModelState.AddModelError("", "Sorry, the email or password entered is invalid.");
            return View(loginData);
        }
        
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(registerData.Username, registerData.Password);
                    //Roles.AddUserToRole(registerData.Username, "trial");
                    return RedirectToAction("Dashboard", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", "Sorry, a user with that email already exists");
                    return View(registerData);
                }
            }
            ModelState.AddModelError("", "Sorry, a user with that email already exists");
            return View(registerData);
        }

        [Authorize]
        public ActionResult Manage()
        {
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                WebSecurity.Logout();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}