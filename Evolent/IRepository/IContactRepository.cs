using Evolent.Models;
using System.Collections.Generic;

namespace Evolent.IRepository
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetContacts();

        Contact GetContact(int contactId);

        bool InsertContact(ContactViewModel contact);

        bool DeleteContact(int contactId);

        bool UpdateContact(ContactViewModel contact);
    }
}
