using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactAppFinal.Data;
using ContactAppFinal.Models;

namespace ContactAppFinal.Controllers
{
    public class ContactDetailsController : Controller
    {
        // GET: ContactDetails
        public ActionResult Index(int id)
        {
            Session["contactId"] = id;
            return View();
        }

        //public ActionResult Details(int id)
        //{
        //    using(var session = NHibernateHelper.CreateSession())
        //    {
        //        var details = session.Query<ContactDetails>().Where(cd=>cd.Contact.Id == id).ToList();
        //        var fulldetails = details.Select(cd => cd.Contact).ToList();
        //        return Json(fulldetails,JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public ActionResult GetData()
        //{
        //    var id = (int)Session["contactId"];

        //    using (var session = NHibernateHelper.CreateSession())
        //    {
        //        var Details = session.Query<ContactDetails>().Where(cd => cd.Contact.Id == id).ToList();
        //        var contactDetailsList = Details.Select(cd => cd.Contact).ToList();
        //        return Json(Details, JsonRequestBehavior.AllowGet);
        //    }
        //}
        public JsonResult GetData(int contactId)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                // Filter the contact details by the contactId
                var contactDetailsList = session.Query<ContactDetails>()
                                                .Where(cd => cd.Contact.Id == contactId)
                                                .Select(cd => new {
                                                    cd.Type,
                                                    cd.Value
                                                }).ToList();

                return Json(new
                {
                    total = 1,
                    page = 1,
                    records = contactDetailsList.Count(),
                    rows = contactDetailsList
                }, JsonRequestBehavior.AllowGet);
            }
        }


        //public ActionResult GetContactDetails(int contactId)
        //{
        //    Session["contactId"] = contactId;
        //    using (var session = NHibernateHelper.CreateSession())
        //    {
        //        var details = session.Query<ContactDetails>().Where(cd => cd.Contact.Id == contactId).ToList();
        //        return View(details);
        //    }
        //}

        public ActionResult Add(ContactDetails contactDetails)
        {
            int id = (int)Session["contactId"];
            using(var session = NHibernateHelper.CreateSession())
            {
                using(var txn  = session.BeginTransaction())
                {
                    contactDetails.Contact.Id = id; 
                    session.Save(contactDetails);
                    txn.Commit();
                    return Json(new { success = true, message = "Contact details added Successfully" });
                }
            }
        }



    }
}