using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactAppFinal.Data;
using ContactAppFinal.Models;

namespace ContactAppFinal.Controllers
{
    
    public class UserController : Controller
    {
        //public ActionResult Index()
        //{
        //    using (var session = NHibernateHelper.CreateSession())
        //    {
        //        return Content("Welcome");
        //    }

        //}

        // GET: User
        [Authorize]
        public ActionResult Index()
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                IQueryable<User> users;
                if (User.IsInRole("Admin"))
                {
                    // Admin can view all users
                    users = session.Query<User>().Where(u => u.IsActive);
                }
                else
                {
                    // Staff can only view their own profile
                    var currentUserName = User.Identity.Name;
                    users = session.Query<User>().Where(u => u.FName == currentUserName && u.IsActive);
                }

                return View(users.ToList());
            }
        }
            
        public ActionResult Details(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var user = session.Get<User>(id);

                if (user == null || !user.IsActive)
                {
                    return HttpNotFound();
                }

                if (!CanAccessUser(user))
                {
                    return new HttpUnauthorizedResult();
                }

                return View(user);
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View(new User());
        }

        // POST: User/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                using (var session = NHibernateHelper.CreateSession())
                using (var transaction = session.BeginTransaction())
                {
                    // Assign role based on IsAdmin flag
                    var roleName = user.IsAdmin ? "Admin" : "Staff";
                    var role = session.Query<Role>().FirstOrDefault(r => r.Name == roleName);
                    if (role == null)
                    {
                        // Create the role if it doesn't exist
                        role = new Role { Name = roleName };
                        session.Save(role);
                    }
                    user.Role = role;

                    // Hash the password (Optional: remove if not hashing)
                    // string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    // user.Password = hashedPassword;

                    user.IsActive = true;

                    session.Save(user);
                    transaction.Commit();
                }

                return RedirectToAction("Index");
            }

            return View(user);
        }
        public ActionResult GetUsers()
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                IQueryable<User> users;
                
                    // Admin can view all users
                    users = session.Query<User>().Where(u => u.IsActive);
                
                //else
                //{
                //    // Staff can only view their own profile
                //    var currentUserName = User.Identity.Name;
                //    users = session.Query<User>().Where(u => u.FName == currentUserName && u.IsActive);
                //}

                return View(users.ToList());
            }
        }


        // GET: User/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    using (var session = NHibernateHelper.CreateSession())
        //    {
        //        var user = session.Get<User>(id);
        //        if (user == null || !user.IsActive)
        //        {
        //            return HttpNotFound();
        //        }

        //        if (!CanAccessUser(user))
        //        {
        //            return new HttpUnauthorizedResult();
        //        }

        //        return View(user);
        //    }
        //}

        // POST: User/Edit/5
        [HttpPost]
            public ActionResult Edit(User user)
            {
                if (ModelState.IsValid)
                {
                    using (var session = NHibernateHelper.CreateSession())
                    using (var transaction = session.BeginTransaction())
                    {
                        var existingUser = session.Get<User>(user.Id);
                        if (existingUser == null || !existingUser.IsActive)
                        {
                            return HttpNotFound();
                        }

                        if (!CanAccessUser(existingUser))
                        {
                            return new HttpUnauthorizedResult();
                        }

                        existingUser.FName = user.FName;
                        existingUser.LName = user.LName;
                        existingUser.IsAdmin = user.IsAdmin;

                        // Update password if it's changed and not empty
                        if (!string.IsNullOrEmpty(user.Password))
                        {
                            // Hash the new password (Optional: remove if not hashing)
                            // string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                            // existingUser.Password = hashedPassword;

                            existingUser.Password = user.Password;
                        }

                        // Update role if changed
                        var roleName = existingUser.IsAdmin ? "Admin" : "Staff";
                        var role = session.Query<Role>().FirstOrDefault(r => r.Name == roleName);
                        if (role == null)
                        {
                            role = new Role { Name = roleName };
                            session.Save(role);
                        }
                        existingUser.Role = role;

                        session.Update(existingUser);
                        transaction.Commit();
                    }

                    return RedirectToAction("Index");
                }

                return View(user);
            }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                // Retrieve the contact by id
                var contact = session.Get<Contact>(id);
                if (contact == null || !contact.IsActive)
                {
                    return HttpNotFound();
                }

                // Pass the contact to the view for editing
                return View(contact);
            }
        }


        // POST: User/AjaxDelete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult EditStatus(int userId, bool IsActive)
        {
            try
            {
                using (var session = NHibernateHelper.CreateSession())
                {
                    var targetUser = session.Query<User>().FirstOrDefault(u => u.Id == userId);
                    using (var txn = session.BeginTransaction())
                    {
                        targetUser.IsActive = IsActive;
                        session.Update(targetUser);
                        txn.Commit();
                        return Json(new { success = true });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult EditAdmin(int userId, bool IsAdmin)
        {
            try
            {
                using (var session = NHibernateHelper.CreateSession())
                {
                    var updatedUser = session.Query<User>().FirstOrDefault(u => u.Id == userId);
                    using (var txn = session.BeginTransaction())
                    {
                        updatedUser.IsAdmin = IsAdmin;
                        session.Update(updatedUser);
                        txn.Commit();
                        return Json(new { success = true });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        // GET: User/Delete/5
        //[Authorize(Roles = "Admin")]
        //public ActionResult Delete(int id)
        //{
        //    using (var session = NHibernateHelper.CreateSession())
        //    {
        //        var user = session.Get<User>(id);
        //        if (user == null || !user.IsActive)
        //        {
        //            return HttpNotFound();
        //        }

        //        return View(user);
        //    }
        //}

        private bool CanAccessUser(User user)
        {
            var currentUserName = User.Identity.Name;

            if (user.FName == currentUserName)
            {
                return true; // User can access their own data
            }

            if (User.IsInRole("Admin"))
            {
                return true; // Admin can access any user
            }

            return false;
        }
    }
}