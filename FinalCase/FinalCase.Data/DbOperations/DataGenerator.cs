using FinalCase.Base.Encryption;
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
                        FirstName="admin",
                        LastName= "admin",
                        Email= "a@gmail.com",
                        Password=  Md5Extension.GetHash("12345"),
                        DateOfBirth =DateTime.Now.AddYears(-23),
                        LastActivityDate=DateTime.Now.AddDays(-2),
                        Role=new Role
                        {
                            Name="Admin"
                        },
                    },
                    new User
                    {
                        IdentityNumber = "11111111112",
                        FirstName = "employee",
                        LastName = "employee",
                        Email = "e@gmail.com",
                        Password = Md5Extension.GetHash("12345"),
                        DateOfBirth = DateTime.Now.AddYears(-23),
                        LastActivityDate = DateTime.Now.AddDays(-2),
                        Role = new Role
                        {
                            Name = "Employee"
                        },
                    }
                    );
                    content.SaveChanges();
                }
                if (!content.Contacts.Any())
                {
                    content.Contacts.AddRange(
                    new Contact
                    {
                        UserId=2,
                        Email="a---@gmail.com",
                        PhoneNumber="11111111111"
                    });
                    content.SaveChanges();
                }
                if (!content.ExpenceNotifies.Any())
                {
                    content.ExpenceNotifies.AddRange(
                    new ExpenceNotify
                    {
                        UserId=1,
                        Explanation="Initial",
                        Amount=100,
                        TransferType="TL",
                        ExpenceType=new ExpenceType
                        {
                            Name="Konaklama",
                            Description="İs surecinde konaklama masraflari."
                        },
                        Documents=new List<Document>
                        {
                            new Document
                            {
                                Description="B--- Otel faturası.",
                                Content=Encoding.ASCII.GetBytes("deneme1")
                            }           
                        }

                    });
                    content.SaveChanges();
                }
                if (!content.ExpenceResponds.Any())
                {
                    content.ExpenceResponds.AddRange(
                    new ExpenceRespond
                    {
                        UserId = 2,
                        Explanation = "Onaylandı",
                        ExpenceNotifyId=1,
                        isApproved=true,
                    });
                    content.SaveChanges();
                }
                if (!content.Accounts.Any())
                {
                    content.Accounts.AddRange(
                    new Account
                    {
                        UserId=2,
                        AccountNumber=11111,
                        IBAN="6345243141",
                        Balance=10000,
                        CurrencyType="Tl",
                        Name="Akbank Business"
                    });
                    content.SaveChanges();
                }
                if (!content.ExpencePayments.Any())
                {
                    content.ExpencePayments.AddRange(
                    new ExpencePayment
                    {
                        ExpenceRespondId = 1,
                        AccountId = 1,
                        Description = "Başarıyla gerçekleşti",
                        TransferType="TL",
                        TransactionDate=DateTime.Now.AddHours(-2),
                        IsDeposited = true,
                    });
                    content.SaveChanges();
                }



            }
        }
    }
}
