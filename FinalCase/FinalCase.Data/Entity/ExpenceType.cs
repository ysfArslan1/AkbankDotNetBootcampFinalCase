using FinalCase.Base.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalCase.Data.Entity
{
    internal class ExpenceType:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
