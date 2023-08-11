using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Base
{
    public class BaseEntity
    {
        public Guid CreateDateTime { get; set; }
        public Guid UpdateDateTime { get; set; }
        public Guid Id { get; set; }
    }
}
