using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPL.DAL.Entities.Concrete
{
    public class Manufacturer : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
