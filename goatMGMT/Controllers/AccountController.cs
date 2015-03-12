using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using goatMGMT.Models;
using WebMatrix.WebData;
using System.Web.Security;
using System.Net.Mail;

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
                if (registerData.Password != registerData.ConfirmPassword) // check if passwords are the same
                {
                    ModelState.AddModelError("", "Sorry, passwords do not match");
                    return View(registerData);
                }
                if (registerData.Password.Length <= 6) // check if password length id greater than 5
                {
                    ModelState.AddModelError("", "Sorry, passwords must be at least 6 characters");
                    return View(registerData);
                }
                try
                {
                    WebSecurity.CreateUserAndAccount(registerData.Username, registerData.Password);
                    Roles.AddUserToRole(registerData.Username, "trialUser");
                    

                    // send email to verify and other things
                    //SmtpClient smtpClient = new SmtpClient("mail.MyWebsiteDomainName.com", 25);

                    //smtpClient.Credentials = new System.Net.NetworkCredential("info@MyWebsiteDomainName.com", "myIDPassword");
                    //smtpClient.UseDefaultCredentials = true;
                    //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //smtpClient.EnableSsl = true;
                    //MailMessage mail = new MailMessage();

                    ////Setting From , To and CC
                    //mail.From = new MailAddress("info@MyWebsiteDomainName", "MyWeb Site");
                    //mail.To.Add(new MailAddress("info@MyWebsiteDomainName"));
                    //mail.CC.Add(new MailAddress("MyEmailID@gmail.com"));

                    //smtpClient.Send(mail);
                    return RedirectToAction("RegistrationConfirmation", "Account");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", "Sorry, a user with that email already exists");
                    return View(registerData);
                }
            }
            // should never get here
            ModelState.AddModelError("", "Sorry, a user with that email already exists");
            return View(registerData);
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel cpIn)
        {
            if (ModelState.IsValid)
            {
                String username = Membership.GetUser().UserName;
                if (cpIn.newPassword != cpIn.newPasswordConfirm) // check if passwords are the same
                {
                    ModelState.AddModelError("", "Sorry, passwords do not match");
                    return View(cpIn);
                }
                if (cpIn.newPassword.Length <= 6) // check if password length id greater than 5
                {
                    ModelState.AddModelError("", "Sorry, passwords must be at least 6 characters");
                    return View(cpIn);
                }
                try
                {
                    WebSecurity.ChangePassword(username, cpIn.oldPassword, cpIn.newPassword);
                    return RedirectToAction("Manage", "Account");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", "Sorry, old password is invalid");
                    return View(cpIn);
                }
            }
            // should never get here
            ModelState.AddModelError("", "Sorry, a user with that email already exists");
            
            return View(cpIn);
        }
        
        [Authorize]
        public ActionResult Manage()
        {
            var currentUser = Membership.GetUser();
            if (currentUser == null)
            {
                return HttpNotFound();
            }
            return View(currentUser);
        }

        public ActionResult RegistrationConfirmation()
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