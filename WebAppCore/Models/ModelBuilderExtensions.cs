using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCore.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Person>().HasData(
                new Person()
                {
                    Id = 1,
                    FirstName = "Mallikarjun",
                    LastName = "Biradar",
                    Age = 33
                },
                new Person()
                {
                    Id = 2,
                    FirstName = "Manjunath",
                    LastName = "Biradar",
                    Age = 31
                });
        }
    }
}
