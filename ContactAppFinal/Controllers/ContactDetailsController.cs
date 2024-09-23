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
    public class ContactDetailsController : Controller
    {
        // GET: ContactDetails
        public ActionResult Index(int id)
        {

            Session["contactId"] = id;
            return View();
        }

        public ActionResult GetData(int page, int rows, string sidx, string sord, bool _search,
            string searchField, string searchString, string searchOper)
        {
            //Console.WriteLine("GetData method received contactId: " + id);
            int contactId = (int)Session["contactId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                // Filter the contact details by the contactId
                var contactDetailsList = session.Query<ContactDetails>()
                                                .Where(cd => cd.Contact.Id == contactId).ToList();
                                                //.Select(cd => new
                                                //{
                                                //    cd.Id,
                                                //    cd.Type,
                                                //    cd.Value
                                                //}).ToList();

                if(_search && searchField == "Value" && searchOper == "eq")
                {
                    contactDetailsList = session.Query<ContactDetails>().Where(p=>p.Value == searchString).ToList();
                }
                int totalCount = contactDetailsList.Count();
                int totalPages = (int)Math.Ceiling((double)totalCount / rows);

                switch (sidx)
                {
                    case "Type":
                        contactDetailsList =sord=="asc" ? contactDetailsList.OrderBy(p=>p.Type).ToList()
                            : contactDetailsList.OrderByDescending(p=>p.Type).ToList();
                        break;
                    case "Value":
                        contactDetailsList = sord == "asc" ? contactDetailsList.OrderBy(p=>p.Value).ToList()
                            : contactDetailsList.OrderByDescending(p => p.Value).ToList();
                        break;
                    default:
                        break;
                }

                var jsonData = new
                {
                    total = totalPages,
                    page = page,
                    records = totalCount,
                    rows = contactDetailsList.Select(detail => new
                    {
                        cell = new string[]
                        {
                            detail.Id.ToString(),
                            detail.Type,
                            detail.Value
                        }
                    }).Skip((page - 1) * rows).Take(rows).ToArray()
                };


                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult GetData(int page, int rows, string sidx, string sord, bool _search,
        //   string searchField, string searchString, string searchOper)
        //{
        //    var id = (int)Session["contactId"];
        //    //var contactDetailsList = User.

        //    using (var session = NHibernateHelper.CreateSession())
        //    {
        //        var Details = session.Query<ContactDetails>().Where(cd => [cd.Contact.Id](http://cd.contact.id/) == id).ToList();
        //        var contactDetailsList = Details;

        //        if (_search && searchField == "Name" && searchOper == "eq")
        //        {
        //            contactDetailsList = Details.Where(p => p.Type == searchString).ToList();
        //        }

        //        int totalCount = Details.Count();

        //        int totalPages = (int)Math.Ceiling((double)totalCount / rows);

        //        switch (sidx)
        //        {
        //            case "Type":
        //                contactDetailsList = sord == "asc" ? contactDetailsList.OrderBy(p => p.Type).ToList()
        //                : contactDetailsList.OrderByDescending(p => p.Type).ToList();
        //                break;

        //            case "Value":
        //                contactDetailsList = sord == "asc" ? contactDetailsList.OrderBy(p => p.Value).ToList()
        //                   : contactDetailsList.OrderByDescending(p => p.Value).ToList();
        //                break;
        //            default:
        //                break;

        //        }

        //        var jsonData = new
        //        {
        //            total = totalPages,
        //            page,
        //            records = totalCount,
        //            rows = contactDetailsList.Select(detail => new
        //            {
        //                id= [detail.Id](http://detail.id/),
        //                cell = new string[]
        //                {
        //                        detail.Id.ToString(),
        //                        detail.Type,
        //                        detail.Value,
        //                }
        //            }).Skip((page - 1) * rows).Take(rows).ToArray()

        //        };
        //        return Json(jsonData, JsonRequestBehavior.AllowGet);
        //    }
        //}








        //public ActionResult GetData(int contactId)
        //{
        //    using (var session = NHibernateHelper.CreateSession())
        //    {
        //        // Filter the contact details by the contactId
        //        var contactDetailsList = session.Query<ContactDetails>()
        //                                        .Where(cd => cd.Contact.Id == contactId)
        //                                        .Select(cd => new {
        //                                            cd.Type,
        //                                            cd.Value
        //                                        }).ToList();

        //        return Json(new
        //        {
        //            total = 1,
        //            page = 1,
        //            records = contactDetailsList.Count(),
        //            rows = contactDetailsList
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}




        public ActionResult Add(ContactDetails contactDetails)
        {
            int id = (int)Session["contactId"];
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    contactDetails.Contact.Id = id;
                    session.Save(contactDetails);
                    txn.Commit();
                    return Json(new { success = true, message = "Contact details added Successfully" });
                }
            }
        }

        public ActionResult Delete(int id)
        {
            using (var session = NHibernateHelper.CreateSession())
            {
                using (var txn = session.BeginTransaction())
                {
                    var contactDetails = session.Get<ContactDetails>(id);
                    session.Delete(contactDetails);
                    txn.Commit();
                    return Json(new { success = true, message = "Product deleted successfully" });
                }
            }
        }

        public ActionResult Edit(ContactDetails contactDetails)
        {
            int id = (int)Session["contactId"];
            using(var session = NHibernateHelper.CreateSession())
            {
                var contactDetailsList = session.Query<ContactDetails>().FirstOrDefault(cd=>cd.Id == contactDetails.Id);
                using (var txn = session.BeginTransaction())
                {
                    if (contactDetailsList != null)
                    {
                        //contactDetailsList.Contact.Id = id;
                        contactDetailsList.Type = contactDetails.Type;
                        contactDetailsList.Value = contactDetails.Value;
                        session.Update(contactDetailsList);
                        txn.Commit();
                    }
                    return Json(new { success = true, message = "ContactDetails edited successfully" });
                }
            }


        }



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

    }
}