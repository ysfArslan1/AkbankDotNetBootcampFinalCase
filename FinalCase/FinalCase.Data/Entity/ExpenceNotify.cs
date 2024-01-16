using FinalCase.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalCase.Data.Entity
{
    public class ExpenceNotify:BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public string Explanation { get; set; }
        public decimal Amount{ get; set; }
        public string TransferType { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
        public int ExpenceTypeId { get; set; }
        public virtual ExpenceType ExpenceType { get; set; }
    }
}
