using AutoMapper;

namespace QPL.BL.Models
{

    public class ProductDefinitionProfile : Profile
    {
        public ProductDefinitionProfile()
        {
            CreateMap<DAL.Entities.Concrete.ProductDefinition, ProductDefinition>();
        }
    }
    public class ProductDefinition
    {
        public int Id { get; set; }
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
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}