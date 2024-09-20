using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactAppFinal.Data;
using ContactAppFinal.Models;

namespace ContactAppFinal.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult GetContacts(int userId)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var user = session.Get<User>(userId);
                if (user == null)
                {
                    return HttpNotFound();
                }

                var contacts = user.Contacts.Where(c => c.IsActive).ToList();

                // Check role and permissions
                bool isAdmin = User.IsInRole("Admin");
                bool isCurrentUser = user.FName == User.Identity.Name;

                if (!isAdmin && !isCurrentUser)
                {
                    return new HttpUnauthorizedResult();
                }

                ViewBag.IsAdmin = isAdmin;
                ViewBag.UserId = userId;
                return View(contacts);
            }
        }

        [HttpGet]
        public ActionResult Create(int userId)
        {
            bool isAdmin = User.IsInRole("Admin");
            if (isAdmin)
            {
                return new HttpUnauthorizedResult();
            }

            var contact = new Contact { User = new User { Id = userId } };
            return PartialView("_addRecordPartial", contact);
        }

        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            bool isAdmin = User.IsInRole("Admin");
            if (isAdmin)
            {
                return new HttpUnauthorizedResult();
            }

            using (var session = NHibernateHelper.CreateSession())
            using (var transaction = session.BeginTransaction())
            {
                contact.IsActive = true;
                contact.User = session.Load<User>(contact.User.Id);
                session.Save(contact);
                transaction.Commit();
            }

            return Json(new { success = true });
        }

    }
}