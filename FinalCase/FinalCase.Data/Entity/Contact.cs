using FinalCase.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalCase.Data.Entity
{
    public class Contact:BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
