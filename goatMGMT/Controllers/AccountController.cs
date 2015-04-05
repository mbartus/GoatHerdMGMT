using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using goatMGMT.Models;
using WebMatrix.WebData;
using System.Web.Security;
using System.Net.Mail;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;

namespace goatMGMT.Controllers
{
    public class AccountController : Controller
    {
        private goatDBEntities db = new goatDBEntities();
        
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
            RegisterViewModel rvm = new RegisterViewModel()
            {
                agreement = db.Licenses.Find(1).Agreement
            };
            return View(rvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<ActionResult> Register(RegisterViewModel registerData)
        {
            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();

            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                ModelState.AddModelError("", "Captcha answer cannot be empty.");
                return View(registerData);
            }

            RecaptchaVerificationResult recaptchaResult = await recaptchaHelper.VerifyRecaptchaResponseTaskAsync();

            if (recaptchaResult != RecaptchaVerificationResult.Success)
            {
                ModelState.AddModelError("", "Incorrect captcha answer.");
                return View(registerData);
            }

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
                catch
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
                catch
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
        [HttpGet]
        public ActionResult ChangeSecurityQuestion()
        {
            ChangeSecurityQuestionViewModel csvm = new ChangeSecurityQuestionViewModel();
            return View(csvm);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangeSecurityQuestion(ChangeSecurityQuestionViewModel vmIn)
        {
            if (ModelState.IsValid)
            {
                int userID = (int)Membership.GetUser().ProviderUserKey;
                UserProfile currentUser = db.UserProfiles.Find(userID);
                currentUser.firstQuestion = vmIn.SecurityQuestion1;
                currentUser.secondQuestion = vmIn.SecurityQuestion2;
                currentUser.firstAnswer = vmIn.SecurityQuestionAnswer1;
                currentUser.secondAnswer = vmIn.SecurityQuestionAnswer2;
                db.Entry(currentUser).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Error", "Home");
                }
                return RedirectToAction("Manage");
            }
            return View(vmIn);
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

        [HttpGet]
        public ActionResult ForgotPassword1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword1(String username)
        {
            // suppress errors from modelstate since we only need username
            if (ModelState.IsValid)
            {
                try
                {
                    UserProfile user = db.UserProfiles.First(m => m.Username == username);
                    return RedirectToAction("ForgotPassword2", new { id = user.UserId });
                }
                catch
                {
                    ModelState.AddModelError("", "Sorry, we couldn't find you. You must have used a different email.");
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult ForgotPassword2(Int32 id)
        {
            UserProfile user = db.UserProfiles.Find(id);
            ChangeSecurityQuestionViewModel csvm = new ChangeSecurityQuestionViewModel()
            {
                SecurityQuestion1 = user.firstQuestion,
                SecurityQuestion2 = user.secondQuestion,
                userID = id
            };

            return View(csvm);
        }

        [HttpPost]
        public ActionResult ForgotPassword2(ChangeSecurityQuestionViewModel csvm)
        {
            if (ModelState.IsValid)
            {
                UserProfile user = db.UserProfiles.Find(csvm.userID);
                if (user.firstAnswer == csvm.SecurityQuestionAnswer1 && user.secondAnswer == csvm.SecurityQuestionAnswer2)
                {
                    var token = WebSecurity.GeneratePasswordResetToken(user.Username);
                    return RedirectToAction("ResetPassword", new { id = user.UserId, t = token});
                }
                else
                {
                    ModelState.AddModelError("", "Your answers are not correct, please try again.");
                    return View(csvm);
                }
            }
            return View(csvm);
        }


        [HttpGet]
        public ActionResult ResetPassword(Int32 id, String t)
        {
            UserProfile user = db.UserProfiles.Find(id);
            RegisterViewModel rvm = new RegisterViewModel()
            {
                Username = user.Username,
                userID = id,
                token = t
            };
            return View(rvm);
        }
        
        [HttpPost]
        public ActionResult ResetPassword(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                if (rvm.Password == rvm.ConfirmPassword)
                {
                    if (rvm.Password.Length > 5)
                    {
                        try
                        {
                            UserProfile user = db.UserProfiles.Find(rvm.userID);
                            WebSecurity.ResetPassword(rvm.token, rvm.Password);
                            return RedirectToAction("Login");
                        }
                        catch
                        {
                            return RedirectToAction("Error", "Home");
                        }
                    }
                    else 
                    {
                        ModelState.AddModelError("", "Passwords must be at least 6 characters");
                        return View(rvm); 
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Passwords do not match, please try again");
                    return View(rvm);
                }
            }
                return View(rvm);
        }


        public ActionResult RegistrationConfirmation()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreateAdmin()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult CreateAdmin(RegisterViewModel registerData)
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
                    Roles.AddUserToRole(registerData.Username, "admin");


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
                    return RedirectToAction("Dashboard", "Home");
                }
                catch
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
        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                WebSecurity.Logout();
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "admin")]
        public ActionResult All()
        {
            
            List<UserList> users = new List<UserList>();
            foreach (UserProfile profile in db.UserProfiles)
            {
                UserList currentUser = new UserList();
                currentUser.username = profile.Username;
                if (db.webpages_Membership.Find(profile.UserId) != null)
                {
                    currentUser.creationDate = (DateTime)db.webpages_Membership.First(m => m.UserId == profile.UserId).CreateDate;
                }
                if (Roles.IsUserInRole(profile.Username,"admin"))
                {
                    currentUser.accountType = "Admin";
                }
                else if (Roles.IsUserInRole(profile.Username,"user"))
                {
                    currentUser.accountType = "Full";
                } 
                else
                {
                    currentUser.accountType = "Trial";
                }
                currentUser.animalcount = db.Animals.Where(m => m.owner == profile.UserId).Count();
                users.Add(currentUser);
            }
            UserViewModel uvm = new UserViewModel()
            {
                userlist = users
            };
            return View(uvm);
        }
    }
}