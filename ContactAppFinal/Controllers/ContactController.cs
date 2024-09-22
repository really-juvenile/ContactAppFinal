using System;
using System.Linq;
using System.Security.Policy;
using System.Web.Mvc;
using ContactAppFinal.Data;
using ContactAppFinal.Models;
using NHibernate.Linq;

namespace ContactAppFinal.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {

        public ActionResult GetContacts()
        {
           

            int userId = (int)Session["userId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                var contacts = session.Query<Contact>()
                    .Where(c => c.User.Id == userId)
                    .Select(c => new Contact
                    {
                        Id = c.Id,
                        FName = c.FName,
                        LName = c.LName,
                        IsActive = c.IsActive,
                    }).
                    ToList();
                return Json(contacts, JsonRequestBehavior.AllowGet);
            }
        }
       

        public ActionResult ContactIndex(int userId)
        {
            Session["userId"] = userId;
            using (var session = NHibernateHelper.CreateSession())
            {
                //var contacts = session.Query<Contact>()
                return View();
            }

        }

        public ActionResult GetContact(int id)
        {
            using(var session = NHibernateHelper.CreateSession())
            {

                var contact = session.Query<Contact>().FirstOrDefault(c => c.Id == id);
                return Json(contact, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Staff")]

        public ActionResult Create(Contact contact)
        {

            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    // Assuming you're using User.Identity.Name for the logged-in user
                    //contact.User = session.Get<User>(int.Parse(User.Identity.Name));
                    int userId = (int)Session["userId"];
                    contact.User.Id = userId;
                    session.Save(contact);
                    txn.Commit();
                    return Json(new { success = true });

                }
                
            }
        }
        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    // Assuming you're using User.Identity.Name for the logged-in user
                    //contact.User = session.Get<User>(int.Parse(User.Identity.Name));
                    int userId = (int)Session["userId"];
                    contact.User.Id = userId;
                    session.Update(contact);
                    txn.Commit();
                    return Json(new { success = true });

                }

            }
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]

        public JsonResult EditContactStatus(int userId, bool isActive)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                var contact = session.Get<Contact>(userId);
                using (var txn = session.BeginTransaction())
                {
                    contact.IsActive = isActive;
                    session.Update(contact);
                    txn.Commit();
                    return Json(new { success = true });
                }
            }
        }
       

    }
}