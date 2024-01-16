using FinalCase.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalCase.Data.Entity
{
    public class Document:BaseEntity
    {
        public int ExpenceNotifyId { get; set; }
        public virtual ExpenceNotify ExpenceNotify { get; set; }
        public string Description { get; set; }
        public byte[] Content  { get; set; }
    }
}
