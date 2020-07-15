using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCore.Models
{
    public class SQLPersonRepository : IPersonRepository
    {
        private readonly AppDBContext context;

        public SQLPersonRepository(AppDBContext context)
        {
            this.context = context;
        }

        public Person Add(Person person)
        {
            if (person != null)
            {
                //person.Id = context.Person.Max(e => e.Id) + 1;
                context.Person.Add(person);
                context.SaveChanges();
            }
            return person;
        }

        public Person Delete(int Id)
        {
            var person = context.Person.Find(Id);

            if (person != null)
            {
                context.Person.Remove(person);
                context.SaveChanges();
            }
            return person;
        }

        public List<Person> GetPeople()
        {
            return context.Person.ToList();
        }

        public Person GetById(int Id)
        {
            return context.Person.Find(Id);            
        }

        public Person Update(Person personChanges)
        {
            var person = context.Person.Attach(personChanges);

            if (person != null)
            {
                person.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            return personChanges;
        }
    }
}
