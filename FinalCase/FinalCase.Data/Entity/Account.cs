using FinalCase.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalCase.Data.Entity
{
    internal class Account:BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int AccountNumber { get; set; }
        public string IBAN { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyType { get; set; }
        public string Name { get; set; }
    }
}
