using Evolent;
using Evolent.Controllers;
using Evolent.IRepository;
using Evolent.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Evolnet.Test
{
    [TestClass]
    public class ContactControllerTest
    {
        ContactController contactController;
        Mock<IContactRepository> contactRepository;

        [TestInitialize]
        public void InitializeTest()
        {
            contactRepository = new Mock<IContactRepository>();
            contactController = new ContactController(contactRepository.Object);
        }

        [TestCleanup]
        public void CleanUp()
        {
            //do cleanup here
        }

        #region Create

        [TestMethod]
        public void ContactController_Create_ReturnsEmptyCreateView()
        {
            var result = contactController.Create() as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void ContactController_Create_NullObject_ReturnsEmptyCreateView()
        {
            ContactViewModel model = null;

            var result = contactController.Create(model) as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void ContactController_Create_NullOrEmptyFirstName_ReturnsEmptyCreateView()
        {
            ContactViewModel model = GetContactModel();
            model.FirstName = null;
            contactController.ModelState.AddModelError("", "Invalid Parameter");

            var result = contactController.Create(model) as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void ContactController_Create_FirstName_LengthGreaterThan50Characters_ReturnsEmptyCreateView()
        {
            ContactViewModel model = GetContactModel();
            model.FirstName = "akjdijwfwfwwwrwrwwrwwwwwwwvsgewwsfwfwcsfwwwrwtefwfeww";
            contactController.ModelState.AddModelError("", "Invalid Parameter");

            var result = contactController.Create(model) as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void ContactController_Create_NullOrEmptyLastName_ReturnsEmptyCreateView()
        {
            ContactViewModel model = GetContactModel();
            model.LastName = string.Empty;
            contactController.ModelState.AddModelError("", "Invalid Parameter");

            var result = contactController.Create(model) as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void ContactController_Create_LastName_LengthGreaterThan50Characters_ReturnsEmptyCreateView()
        {
            ContactViewModel model = GetContactModel();
            model.LastName = "akjdijwfwfwwwrwrwwrwwwwwwwvsgewwsfwfwcsfwwwrwtefwfeww";
            contactController.ModelState.AddModelError("", "Invalid Parameter");

            var result = contactController.Create(model) as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void ContactController_Create_NullOrEmptyEmail_ReturnsEmptyCreateView()
        {
            ContactViewModel model = GetContactModel();
            model.Email = string.Empty;
            contactController.ModelState.AddModelError("", "Invalid Parameter");

            var result = contactController.Create(model) as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void ContactController_Create_Email_LengthGreaterThan100Characters_ReturnsEmptyCreateView()
        {
            ContactViewModel model = GetContactModel();
            model.Email = "akjdijwfwfwwwrwrwwrwwwwwwwvsgewwsfwfwcsfwwwrwtefwfeww@testttfsfksfjdijiqjeiqjeiqeqieqeiwqqqwndqnwjenjqwejtttt.com";
            contactController.ModelState.AddModelError("", "Invalid Parameter");

            var result = contactController.Create(model) as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void ContactController_Create_Email_InvalidEmail1_ReturnsEmptyCreateView()
        {
            ContactViewModel model = GetContactModel();
            model.Email = "test@@test.com";
            contactController.ModelState.AddModelError("", "Invalid Parameter");

            var result = contactController.Create(model) as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void ContactController_Create_Email_InvalidEmail2_ReturnsEmptyCreateView()
        {
            ContactViewModel model = GetContactModel();
            model.Email = "test@test.com1.";
            contactController.ModelState.AddModelError("", "Invalid Parameter");

            var result = contactController.Create(model) as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void ContactController_Create_Email_InvalidEmail3_ReturnsEmptyCreateView()
        {
            ContactViewModel model = GetContactModel();
            model.Email = "test@test.com.co.in";
            contactController.ModelState.AddModelError("", "Invalid Parameter");

            var result = contactController.Create(model) as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void ContactController_Create_Email_InvalidEmail4_ReturnsEmptyCreateView()
        {
            ContactViewModel model = GetContactModel();
            model.Email = "123@123.99";
            contactController.ModelState.AddModelError("", "Invalid Parameter");

            var result = contactController.Create(model) as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void ContactController_Create_ValidObject_FailedToCreateContact_ReturnsEmptyCreateView()
        {
            ContactViewModel model = GetContactModel();

            contactRepository.Setup(x => x.InsertContact(It.IsAny<ContactViewModel>())).Returns(false);

            var result = contactController.Create(model) as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void ContactController_Create_ValidObject_CreatedContactSuccessfully_ReturnsContactsList()
        {
            ContactViewModel model = GetContactModel();

            contactRepository.Setup(x => x.InsertContact(It.IsAny<ContactViewModel>())).Returns(true);
            //contactRepository.Setup(x => x.GetContacts()).Returns(GetContacts());

            var result = contactController.Create(model) as RedirectToRouteResult;

            Assert.AreEqual("Read", result.RouteValues["action"]);
        }

        #endregion

        #region Read

        [TestMethod]
        public void ContactController_Read_ReturnsEmptyContactsList()
        {
            contactRepository.Setup(x => x.GetContacts()).Returns(new List<Contact>());

            var result = contactController.Read() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ContactController_Read_ReturnsContactsList()
        {
            contactRepository.Setup(x => x.GetContacts()).Returns(GetContacts());

            var result = contactController.Read() as ViewResult;

            Assert.IsNotNull(result);
        }

        #endregion

        #region Private

        private ContactViewModel GetContactModel()
        {
            return new ContactViewModel
            {
                Email = "PravinKumre@gmail.com",
                FirstName = "Pravin",
                LastName = "Kumre"
            };
        }

        private List<Contact> GetContacts()
        {
            return new List<Contact>
            {
                new Contact
                {
                    Active = true,
                    ContactId = 1,
                    Email = "PravinKumre@gmail.com",
                    FirstName = "Pravin",
                    LastName = "Kumre"
                },
                new Contact
                {
                    Active = false,
                    ContactId = 1,
                    Email = "test@gmail.com",
                    FirstName = "TestF",
                    LastName = "TestL"
                }
            };
        }

        #endregion
    }
}
