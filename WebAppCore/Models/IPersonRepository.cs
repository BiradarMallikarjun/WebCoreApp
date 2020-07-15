using System.Collections.Generic;

namespace WebAppCore.Models
{
    public interface IPersonRepository
    {
        Person GetById(int Id);

        List<Person> GetPeople();

        Person Add(Person person);

        Person Update(Person person);

        Person Delete(int Id);
    }
}