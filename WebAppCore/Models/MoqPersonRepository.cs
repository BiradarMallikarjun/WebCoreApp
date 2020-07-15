using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCore.Models
{
    public class MoqPersonRepository : IPersonRepository
    {        
        private List<Person> _people;

        public MoqPersonRepository()
        {
            _people = new List<Person>()
            {
                new Person() { Id = 1, FirstName = "Maha", LastName = "Biradar", Age = 22 },
                new Person() { Id = 2, FirstName = "Manju", LastName = "Biradar", Age = 32 }
            };
        }

        public Person Add(Person person)
        {
            person.Id = _people.Max(e=>e.Id) + 1;             
            _people.Add(person);
            return person;
        }

        public Person Delete(int Id)
        {
            var person = _people.Find(e => e.Id == Id);
            if (person != null)
            {
                _people.Remove(person);
            }
            return person;
        }

        public List<Person> GetPeople()
        {
            return _people.ToList();
        }

        public Person GetById(int Id = 1)
        {
            return _people.FirstOrDefault(e => e.Id == Id);
        }

        public Person Update(Person personChanges)
        {
            var person = _people.Find(e => e.Id == personChanges.Id);

            if (person != null)
            {
                person.FirstName = personChanges.FirstName;
                person.LastName = personChanges.LastName;
                person.Age = personChanges.Age;
            }
            return person;           
        }
    }
}
