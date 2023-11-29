using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPL.DAL.Entities.Concrete
{
    public class ProductDefinition : BaseEntity
    {
        public string CPC { get; set; } = string.Empty;
        public string NortelCpc { get; set; } = string.Empty;
        public string EngineeringCode { get; set; } = string.Empty;
        public string SpecNo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public string Concept { get; set; } = string.Empty;
        public string PackageType { get; set; } = string.Empty;
        public string RoshStatus { get; set; } = string.Empty;
        public string FlammabilityRating { get; set; } = string.Empty;
        public string RadiassionRating { get; set; } = string.Empty;
        public bool DisassembledOrReusable { get; set; }

        //public DateTime? EntryDate { get; set; }
        //public DateTime? ModifyDate { get; set; }
        //public DateTime? TDate { get; set; }
        //public string? IbisFile { get; set; }
        //public string? BsdlFile { get; set; }
        //public string? Conclass { get; set; }
        //public string? CbdsCode { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
