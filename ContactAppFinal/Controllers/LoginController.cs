using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ContactAppFinal.Data;
using ContactAppFinal.Models;
using ContactAppFinal.ViewModels;

namespace ContactAppFinal.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View(new LoginVM());
        }
        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var user = session.Query<User>().FirstOrDefault(u => u.FName == loginVM.Username); //circular ref


                if (user != null && user.IsActive)
                {

                    bool isPasswordValid = loginVM.Password == user.Password;
                    if (isPasswordValid)
                    {
                        Session["userId"] = user.Id;
                        FormsAuthentication.SetAuthCookie(user.FName, false);
                        return RedirectToAction("Index", "User");
                    }

                }
                ModelState.AddModelError("", "INvalid username or password");
                return View(loginVM);
            }
        }
        
        public ActionResult Register()
        {
            return View(new RegisterVM());
        }
        
        [HttpPost]
        public ActionResult Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                using (var session = NHibernateHelper.CreateSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        // Hash the password using BCrypt
                        //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerVM.Password);

                        
                        var user = new User
                        {
                            FName = registerVM.FName,
                            LName = registerVM.LName,
                            Password = registerVM.Password,
                            IsAdmin = registerVM.IsAdmin,
                            IsActive = true,
                           

                        };

                        var role = new Role
                        {
                            Name = registerVM.IsAdmin ? "Admin" : "Staff",
                            User = user
                        };
                        user.Role = role;
                        session.Save(user);
                        transaction.Commit();
                    }
                }

                return RedirectToAction("Login");
            }

            return View(registerVM);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}