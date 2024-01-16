using FinalCase.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalCase.Data.Entity
{
    public class User:BaseEntity
    {
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastActivityDate { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
