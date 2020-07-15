using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCore.ViewModel
{
    public class PersonCreateViewModel
    {        
        [Required]
        [MaxLength(250)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(250)]
        public string LastName { get; set; }
        [Required]
        [Range(1, 150)]
        public int Age { get; set; }

        public IFormFile Photo { get; set; }
    }
}
