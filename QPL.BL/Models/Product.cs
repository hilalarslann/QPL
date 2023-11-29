using AutoMapper;

namespace QPL.BL.Models
{

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<DAL.Entities.Concrete.Product, Product>();
        }
    }
    public class Product
    {
        public int Id { get; set; }
        public string CPC { get; set; } = string.Empty;
        public string EngineeringCode { get; set; } = string.Empty;
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; } = string.Empty;
        public string ManufacturerCode { get; set; } = string.Empty;
        public string PreviousEngineeringStatus { get; set; } = string.Empty;
        public string CurrentEngineeringStatus { get; set; } = string.Empty;
        public int ProductDefinitionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}