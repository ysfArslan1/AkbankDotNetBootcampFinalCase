using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalCase.Data.Entity
{
    public class ExpencePayment
    {
        public int ExpenceRespondId { get; set; }
        public virtual ExpenceRespond ExpenceRespond { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public string Description{ get; set; }
        public string TransferType{ get; set; }
        public string TransactionDate { get; set; }
        public bool IsDeposited { get; set; }
    }
}
