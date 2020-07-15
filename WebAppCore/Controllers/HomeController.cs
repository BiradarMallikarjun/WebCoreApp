using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using WebAppCore.Models;
using WebAppCore.ViewModel;

namespace WebAppCore.Controllers
{
    public class HomeController : Controller
    {
        private IPersonRepository _personRepository;
        private readonly IWebHostEnvironment env;

        public HomeController(IPersonRepository personRepository, IWebHostEnvironment env)
        {            
            _personRepository = personRepository;
            this.env = env;
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var per = _personRepository.GetById(id);
            return View(per);
        }
        [HttpGet]
        public IActionResult Index()
        {
            var per = _personRepository.GetPeople();
            return View(per);
        }

        [HttpGet]
        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult Create(PersonCreateViewModel person)
        {           
            if (person != null && ModelState.IsValid )
            {
                string uniqueFileName = ProcessUploadFile(person);
                Person per = new Person()
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Age = person.Age,
                    PhotoPath = uniqueFileName,
                };
                _personRepository.Add(per);
                return RedirectToAction("Details", new { Id = per.Id });
            }
            return View();
        }

        private string ProcessUploadFile(PersonCreateViewModel person)
        {
            string uniqueFileName = null;
            if (person.Photo != null)
            {
                string destinationFolder = Path.Combine(env.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + person.Photo.FileName;
                string filePath = Path.Combine(destinationFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    person.Photo.CopyTo(stream);
                }
            }

            return uniqueFileName;
        }

        [HttpGet]
        public IActionResult Update(int Id)
        {
            Person person = _personRepository.GetById(Id);
            PersonUpdateViewModel model = null;
            if (person != null)
            {
                model = new PersonUpdateViewModel()
                {
                    Id = person.Id,
                    FirstName=person.FirstName,
                    LastName = person.LastName,
                    Age = person.Age,
                    ExistingPhotoPath = person.PhotoPath                   
                };
                return View(model);
            }            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(PersonUpdateViewModel person)
        {
            if (person != null && ModelState.IsValid)
            {
                Person newPerson = new Person()
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Age = person.Age
                };

                if (person.Photo != null)
                {
                    if (person.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(env.WebRootPath,"images" ,person.ExistingPhotoPath);                        
                        System.IO.File.Delete(filePath);
                    }
                    newPerson.PhotoPath = ProcessUploadFile(person);
                }

                _personRepository.Update(newPerson);
                return RedirectToAction("Details", new { Id = newPerson.Id });
            }
            return View();
        }
       
        [HttpPost]
        public IActionResult Delete(Person person)
        {           
            if (person != null && person.Id > 0)
            {
                _personRepository.Delete(person.Id);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Person person = _personRepository.GetById(Id);
            if (person != null)
            {                
                return View(person);
            }
            return View("Index");
        }
    }
}
