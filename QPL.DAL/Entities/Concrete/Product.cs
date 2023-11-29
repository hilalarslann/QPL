using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPL.DAL.Entities.Concrete
{
    public class Product : BaseEntity
    {
        public string EngineeringStatus { get; set; } = string.Empty;
        public string PrevEngineeringStatus { get; set; } = string.Empty;
        public int ManufacturerId { get; set; }
        public string ManufacturerCode { get; set; } = string.Empty;
        public int ProductDefinitionId { get; set; }
        public virtual Manufacturer? Manufacturer { get; set; }
        public virtual ProductDefinition? ProductDefinition { get; set; }
    }
}
