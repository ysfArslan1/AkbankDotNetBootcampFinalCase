using FinalCase.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalCase.Data.Entity
{
    internal class ExpenceRespond:BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ExpenceNotifyId { get; set; }
        public virtual ExpenceNotify ExpenceNotify { get; set; }
        public bool isApproved { get; set; }
        public string Explanation { get; set; }
    }
}
