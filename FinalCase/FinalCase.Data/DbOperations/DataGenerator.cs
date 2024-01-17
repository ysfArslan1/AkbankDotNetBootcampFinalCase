using FinalCase.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FinalCase.Data.DbOperations
{
    public class DataGenerator
    {
        //Database de data üretmek içinkullanılıyor
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var content = new VbDbContext(serviceProvider.GetRequiredService<DbContextOptions<VbDbContext>>()))
            {
                if (!content.Users.Any())
                {
                    content.Users.AddRange(
                    new User
                    {
                        IdentityNumber="11111111111",
                        FirstName="A-",
                        LastName="D-",
                        DateOfBirth=DateTime.Now.AddYears(-23),
                        LastActivityDate=DateTime.Now.AddDays(-2),
                        Role=new Role
                        {
                            Name="Admin"
                        },
                    }
                    );

                    content.SaveChanges();
                }

                

            }
        }
    }
}
