using Evolent.IRepository;
using Evolent.Models;
using System.Collections.Generic;
using System.Linq;

namespace Evolent.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly demoCRUDEntities context;

        public ContactRepository(demoCRUDEntities dbContext)
        {
            context = dbContext;
        }

        public bool DeleteContact(int contactId)
        {
            var data = context.Contacts.FirstOrDefault(x => x.ContactId == contactId);
            if (data != null)
            {
                context.Contacts.Remove(data);
                context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public Contact GetContact(int contactId)
        {
            return context.Contacts.Where(x => x.ContactId == contactId).SingleOrDefault();
        }

        public IEnumerable<Contact> GetContacts()
        {
            return context.Contacts.ToList();
        }

        public bool InsertContact(ContactViewModel contact)
        {
            context.Contacts.Add(ConvertToDTOObject(contact));
            context.SaveChanges();
            return true;
        }

        private Contact ConvertToDTOObject(ContactViewModel contact)
        {
            return new Contact
            {
                Active = contact.Active,
                ContactId = contact.ContactId,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName
            };
        }

        public bool UpdateContact(ContactViewModel contact)
        {
            var data = context.Contacts.FirstOrDefault(x => x.ContactId == contact.ContactId);
            if (data != null)
            {
                data.FirstName = contact.FirstName;
                data.LastName = contact.LastName;
                data.Email = contact.Email;
                data.Active = contact.Active;
                context.SaveChanges();
                return true;
            }
            else
                return false;
        }
    }
}