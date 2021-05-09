using Evolent.IRepository;
using Evolent.Models;
using System.Web.Mvc;

namespace Evolent.Controllers
{
    public class ContactController : Controller
    {
        IContactRepository contactRepository;

        public ContactController()
        {
        }

        public ContactController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(ContactViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                if (contactRepository.InsertContact(model))
                {
                    ViewBag.Message = "Contact created successfully";
                    return RedirectToAction("Read");
                }
            }
            return View("Create");
        }

        [HttpGet]
        public ActionResult Read()
        {
            return View(contactRepository.GetContacts());
        }

        public ActionResult Update(int contactId)
        {
            if (contactId == 0)
                return RedirectToAction("Read");

            return View(contactRepository.GetContact(contactId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ContactViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                if (contactRepository.UpdateContact(model))
                    return RedirectToAction("Read");
            }
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int contactId)
        {
            if (contactId == 0)
                return RedirectToAction("Read");

            if (contactRepository.DeleteContact(contactId))
                return RedirectToAction("Read");
            else
                return View();
        }
    }
}