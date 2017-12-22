using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVCPersonApp.Models;
namespace MVCPersonApp.Controllers
{
    public class PersonController : Controller
    {
        IList<PersonModel> personList = new List<PersonModel>() {
                    new PersonModel(){ Id=1, FirstName="John", LastName = "Smith" }
                };

        // GET List: Person
        public ActionResult List()
        {
            return View();
        }

        // GET Details: Person
        public ActionResult Details()
        {
            if (Session["Person"] != null)
            {
                PersonModel model = (PersonModel)Session["Person"];
                return View(model);
            }
            else
            {
                var persn = personList.Where(s => s.Id == 1).FirstOrDefault();
                if (persn == null)
                {
                    return HttpNotFound();
                }
                return View(persn);
            }
        }

        public ActionResult Edit(int? PersonId)
        {
            if (PersonId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Session["Person"] != null) { PersonModel model = (PersonModel)Session["Person"]; return View(model); }
            else
            {
                var persn = personList.Where(s => s.Id == PersonId).FirstOrDefault();
                if (persn == null)
                {
                    return HttpNotFound();
                }
                return View(persn);
            }
        }

        [HttpPost]
        public ActionResult Edit(PersonModel model)
        {
            if (ModelState.IsValid)
            {
                Session["Person"] = model;
                return RedirectToAction("Details");
            }
            return View(model);
        }

        public ActionResult Index()
        {
            return View(personList);
        }
    }
}